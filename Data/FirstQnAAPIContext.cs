using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FirstQnAAPI;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstQnAAPI.Data
{
    public class FirstQnAAPIContext : DbContext
    {
        public FirstQnAAPIContext (DbContextOptions<FirstQnAAPIContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChoicesMaster>()
               .HasNoKey();
            // modelBuilder.Entity<QResult>()
            //    .HasNoKey();
            // modelBuilder.Entity<QWiseResult>()
            //    .HasNoKey();

            //.HasOne(p => p.Blog)
            //.WithMany(b => b.Posts)
            //.HasForeignKey(p => p.BlogUrl)
            //.HasPrincipalKey(b => b.Url);
        }



        public DbSet<Group> Group { get; set; }

        public DbSet<QBMaster> QBMaster {get; set;}

        public DbSet<ChoicesMaster> ChoicesMaster {get; set;}

        public DbSet<QResult> QResult {get; set;}

        public DbSet<QWiseResult> QWiseResult {get; set;}

        public UserResult UserResult {get; set;}

    }


    [PrimaryKey("QuestionId")]
    public class QBMaster
    {
        [Required]
        [Column("QuestionId")]
        public int QuestionId {  get; set; }

        [Required]
        public required string QuestionText { get; set; }

        [Required]
        public int GroupId { get; set; }

        [Required]
        public int AnswerChoiceId {get; set;}

        [Required]
        public int ChoiceType { get; set; }

    }

    public class Group
    { 
        public int Id { get; set; }

        public required string GroupName { get; set; }

        public string? GroupSummary { get; set; }

        public int CountByGroup { get; set; }

    }

    public class ChoicesMaster
    {
        [Required]
        public int QuestionId {  get; set; }

        [Required]
        public int ChoiceId { get; set; }

        [Required]
        public required string ChoiceText  { get; set; }

    }

    public class UserResult
    {
            // //results: attemptResults, // should be an array containing attemptId, score, resultsArray[{id: 1, answer:4, selected:'4'}, {id:2, answer:3, selected:'2'}...]  
            // //questionid, answer, selected
           // public int id {get; set;} //attemptid - later create it in the table as attemptid and map it as id as already id identity column exists

            public required string UserId {get; set;}

            public int GroupId {get; set;}

            public int TimeSpent {get; set;}

            public int Score { get; set; }

            public int AttemptId {get; set;}
      //  public QResult Result {get; set;}
        public QuestionWiseResult[] Results {get; set;}   
    }

  
   public class QuestionWiseResult
   {
     public int id {get; set;}

        public int answer {get; set;}

        public int selected {get; set;}
   
   }

    [PrimaryKey("Id")]
    public class QResult
    {
        public int Id {get; set;}
        [Required]
        public required string UserId {get; set;}

        public int GroupId {get; set;}

        public int TimeSpent {get; set;}

        public int Score { get; set; }

        public int AttemptId {get; set;}

    }

    [PrimaryKey("Id")]
    public class QWiseResult
    {
        
        public int Id {get; set;}

        [Required]
        public required string UserId {get; set;}

        public int GroupId {get; set;}

        public int QuestionId {get; set;}

        public int AnswerChoiceId {get; set;}

        public int SelectedChoiceId {get; set;}

        public int AttemptId {get; set;}
    }
}
