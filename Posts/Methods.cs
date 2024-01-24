using Posts;
using Posts.Exceptions;
using System.Data.SqlClient;
using System.Text.Json;
public static class Methods
{
    public static async Task AddPostToDatabaseAsync(int id)
    {
        string connectionString = @"Data Source=ENEL\SQLEXPRESS;Initial Catalog=AdoNet;Integrated Security=True;";
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
                string apiResponse = await httpClient.GetStringAsync($"https://jsonplaceholder.typicode.com/posts/{id}");
                Post? post =
                JsonSerializer.Deserialize<Post>(apiResponse);
                string query = $"Insert into Posts(id,userId,title,body)" +
                $"Values(@id,@userId,@title,@body)";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", post.id);
                    cmd.Parameters.AddWithValue("@userId", post.userId);
                    cmd.Parameters.AddWithValue("@title", post.title);
                    cmd.Parameters.AddWithValue("@body", post.body);
                    affectedRow = (int)cmd.ExecuteNonQuery();
                    if (affectedRow > 0)
                        Console.WriteLine("Sucessfully added");
                }
            }
        }
    }

    public static async Task GetMissingPostsFromApiAsync()
    {
        string connectionString = @"Data Source=ENEL\SQLEXPRESS;Initial Catalog=AdoNet;Integrated Security=True;";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            List<int> existingPostIds = GetExistingPostIds(connection);
            using (HttpClient httpClient = new HttpClient())
            {
                string apiResponse = await httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/posts");
                List<Post> apiPosts = JsonSerializer.Deserialize<List<Post>>(apiResponse);
                List<Post> missingPosts = apiPosts.Where(post => !existingPostIds.Contains(post.id)).ToList();
                if (missingPosts.Count == 0)
                {
                    throw new CannotBeNullException("There is not any missing post");
                }
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
    public static async Task<int> GetPostCountOfUserAsync(int userId)
    {
        string connectionString = @"Data Source=ENEL\SQLEXPRESS;Initial Catalog=AdoNet;Integrated Security=True;"; 
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = $"Select Count(*) From Posts Where userId={userId}";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                int postCount = (int)cmd.ExecuteScalar();
                Console.WriteLine(postCount);
                return postCount;
            }
        }
    }
}
