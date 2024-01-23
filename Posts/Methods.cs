using Posts;
using Posts.Exceptions;
using System.Data.SqlClient;
using System.Text.Json;
public static class Methods
{
    public static async Task AddPostToDatabase(int id)
    {
        string connectionString = @"Server=ENEL\SQLEXPRESS;Database=Posts;Trusted_Connection=true;";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string checkQuery = $"Select Count(*) From Posts Where Id={id}";
            using (SqlCommand cmd = new SqlCommand(checkQuery, connection))
            {
                int existCount = (int)cmd.ExecuteScalar();
                if (existCount > 0)
                {
                    throw new AlreadyExistException($"Post with Id {id} already exists in the database");
                }
            }
            connection.Close();
            using (HttpClient httpClient = new HttpClient())
            {
                connection.Open();
                int affectedRow = 0;
                string response = await httpClient.GetStringAsync($"https://jsonplaceholder.typicode.com/posts/{id}");
                List<Post>? JsonPosts = JsonSerializer.Deserialize<List<Post>>(response);
                string query = $"Insert into Posts(id,userId,title,body)" +
                $"Values(@json.id,@json.userId,@json.title,@json.body)";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    affectedRow = (int)cmd.ExecuteNonQuery();
                    if (affectedRow > 0)
                        Console.WriteLine("Sucessfully added");
                }
            }
        }
    }
    public static async Task GetMissingPostsFromApi()
    {
        string connectionString = @"Data Source=ENEL\SQLEXPRESS;Initial Catalog=Posts;Integrated Security=True;";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            List<int> existingPostIds = GetExistingPostIds(connection);
            using (HttpClient httpClient = new HttpClient())
            {
                string apiResponse = await httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/posts");
                List<Post> apiPosts = JsonSerializer.Deserialize<List<Post>>(apiResponse);
                List<Post> missingPosts = apiPosts.Where(post => !existingPostIds.Contains(post.id)).ToList();
                foreach (var missingPost in missingPosts)
                {
                    Console.WriteLine($"UserId:{missingPost.userId}; Id:{missingPost.id};Title:{missingPost.title}; Body:{missingPost.body}");
                }
            }
            static List<int> GetExistingPostIds(SqlConnection connection)
            {
                List<int> existingPostIds = new List<int>();
                string query = "SELECT Id FROM Posts";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        existingPostIds.Add(reader.GetInt32(0));
                    }
                }
                return existingPostIds;
            }
        }
    }
    public static async Task<int> GetPostCountOfUser(int userId)
    {
        string connectionString = @"Data Source=ENEL\SQLEXPRESS;Initial Catalog=Posts;Integrated Security=True;"; ;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = $"Select Count(*) From Posts Where userId={userId}";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                int postCount = (int)cmd.ExecuteScalar();
                return postCount;
            }
        }
    }
}


