namespace SchoolProject.Data.Entities
{
    public class InstructorSubject
    {
        [Key]
        public int InsId { get; set; }
        [Key]
        public int SubId { get; set; }

        [ForeignKey(nameof(InsId))]
        [InverseProperty(nameof(Entities.Instructor.InstSubjects))]
        public Instructor? Instructor { get; set; }

        [ForeignKey(nameof(SubId))]
        [InverseProperty(nameof(Entities.Subject.InstSubjects))]
        public Subject? Subject { get; set; }
    }
}
