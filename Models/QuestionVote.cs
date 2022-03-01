using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StackOverflow.Models
{
    public class QuestionVote : Vote 
    {
        public int QuestionId { get; set; }

        [JsonIgnore]
        [ForeignKey("QuestionId")]
        public Question? Question { get; set; }
    }
}
