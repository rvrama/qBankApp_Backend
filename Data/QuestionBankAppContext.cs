using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuestionBankApp.Data
{
    public class QuestionBankAppContext : DbContext
    {
        public QuestionBankAppContext (DbContextOptions<QuestionBankAppContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChoicesMaster>().ToTable("ChoicesMaster")
               .HasNoKey();
            modelBuilder.Entity<QBMaster>().ToTable("QBMaster");
            modelBuilder.Entity<GroupMaster>().ToTable("GroupMaster");
            modelBuilder.Entity<QResult>().ToTable("QResult");
            modelBuilder.Entity<QWiseResult>().ToTable("QWiseResult");
        }
        public DbSet<GroupMaster> GroupMaster { get; set; }

        public DbSet<QBMaster> QuestionBankMaster {get; set;}

        public DbSet<ChoicesMaster> ChoicesMaster {get; set;}

        public DbSet<QResult> QResults {get; set;}

        public DbSet<QWiseResult> QuestionwiseResults {get; set;}

        public UserResult UserResult {get; set;}

    }

#region "Entity Models"

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

    public class GroupMaster
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


#endregion

#region "custom model"

    public class UserResult
    {
            public required string UserId {get; set;}

            public int GroupId {get; set;}

            public int TimeSpent {get; set;}

            public int Score { get; set; }

            public int AttemptId {get; set;}
      //  public QResult Result {get; set;}
        public QuestionWiseResult[] Results {get; set;}   
    }

  
   public class QuestionWiseResult //used only to return the response.  This can be internal Model at the controller level too
   {
     public int id {get; set;}

        public int answer {get; set;}

        public int selected {get; set;}
   
   }

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

    // public class ChoiceModel{
    //     public int choiceId;

    //     [Required]
    //     public required string choice;
    // }

#endregion

}
