using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StackOverflow.Models
{
    public class AnswerVote : Vote 
    {
        public int AnswerId { get; set; }

        [JsonIgnore]
        [ForeignKey("AnswerId")]
        public Answer? Answer { get; set; }
    }
}
