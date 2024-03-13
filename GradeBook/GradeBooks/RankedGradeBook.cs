using System;
using System.Linq;

namespace GradeBook.GradeBooks;

public class RankedGradeBook:BaseGradeBook
{
    public RankedGradeBook(string name) : base(name)
    {
        Type = Enums.GradeBookType.Ranked;
    }

    public override char GetLetterGrade(double averageGrade)
    {
        if (Students.Count < 5)
        {
            throw new InvalidOperationException("Ranked grading requires at least 5 students.");
        }
        //Provide the appropriate grades based on how the input grade compares to other students.
        // _(One way to solve this is to figure out how many students make up 20%, then loop through all the grades and check how many scored higher than the input average, every N students where N is that 20% value drop a letter grade.)_
        // - If there are less than 5 students throw an `InvalidOperationException`.
        // - Return A if the input grade is in the top 20% of the class.
        // - Return B if the input grade is between the top 20 and 40% of the class.
        // - Return C if the input grade is between the top 40 and 60% of the class.
        // - Return D if the input grade is between the top 60 and 80% of the class.
        // - Return F if the grade is below the top 80% of the class.
        var grades = Students.OrderByDescending(e => e.AverageGrade).Select(e => e.AverageGrade).ToList();
        for (int i = 0; i < grades.Count; i++)
        {
            if (averageGrade >= grades[i])
            {
                if (i < grades.Count * 0.2)
                {
                    return 'A';
                }
                else if (i < grades.Count * 0.4)
                {
                    return 'B';
                }
                else if (i < grades.Count * 0.6)
                {
                    return 'C';
                }
                else if (i < grades.Count * 0.8)
                {
                    return 'D';
                }
            }
        }
        return 'F';
    }

    // [ ] Override `RankedGradeBook`'s `CalculateStatistics` method
    // - [ ] Short circuit the method if there are less than 5 students.
    // - If there are less than 5 students write "Ranked grading requires at least 5 students." to the Console.
    // - If there are 5 or more students call the base class's `CalculateStatistics` method using `base.CalculateStatistics`.

    public override void CalculateStatistics()
    {
        if (Students.Count < 5)
        {
            Console.WriteLine("Ranked grading requires at least 5 students.");
            return;
        }
        base.CalculateStatistics(); 
    }
    public override void CalculateStudentStatistics(string name)
    {
        if (Students.Count < 5)
        {
            Console.WriteLine("Ranked grading requires at least 5 students.");
            return;
        }
        base.CalculateStudentStatistics(name);
    }
}
