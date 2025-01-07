using System.ComponentModel.DataAnnotations;

namespace FirstQnAAPI.Data
{
    public class QuestionList
    {
        [Required]
        public int questionId {get; set;}

        [Required]
        public required string questionTxt {get; set;}

        [Required]
        public required string choices {get; set;}

        public int groupId{get; set;}

        public int choiceType {get; set; }
 
        public int answerChoiceId {get; set;}

    }

    public class ChoiceModel{
        public int choiceId;

        [Required]
        public required string choice;
    }
}