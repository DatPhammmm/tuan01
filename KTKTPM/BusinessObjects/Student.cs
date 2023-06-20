using System;
namespace BusinessObjects
{
    [Serializable] //chú ý đối tượng phải được Serialize
    public class Student
    {
        public long StudentId { get; set; }
        public string FullName { get; set; }
        public DateTime DOB { get; set; }
        public Student() : this(0, "no-name", new DateTime())
        {
        }
        public Student(long id, string fname, DateTime dob)
        {
            StudentId = id; FullName = fname; DOB = dob;
        }
        public override string ToString()
        {
            return FullName + "\t" + DOB;
        }
    }
}