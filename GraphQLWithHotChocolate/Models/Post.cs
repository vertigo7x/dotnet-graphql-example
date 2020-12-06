using System.Text.Json.Serialization;

namespace GraphQLWithHotChocolate.Models {
    public class Post {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
        public int AuthorId { get; set; }
        // Añadido para evitar la referencia cíclica
        [JsonIgnore]
        public Blog Blog { get; set; }

        [JsonIgnore]
        public Author Author { get; set; }
    }
}