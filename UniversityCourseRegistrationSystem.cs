// using System;
// using System.Collections.Generic;
// using System.Linq;


// public interface IStudent
// {
//     int StudentId { get; }
//     string Name { get; }
//     int Semester { get; }
// }

// public interface ICourse
// {
//     string CourseCode { get; }
//     string Title { get; }
//     int MaxCapacity { get; }
//     int Credits { get; }
// }


// public class EnrollmentSystem<TStudent, TCourse>
//     where TStudent : IStudent
//     where TCourse : ICourse
// {
//     private readonly Dictionary<TCourse, List<TStudent>> _enrollments = new();

//     public bool EnrollStudent(TStudent student, TCourse course, out string reason)
//     {
//         if (student == null || course == null)
//         {
//             reason = "Student or course cannot be null.";
//             return false;
//         }

//         if (!_enrollments.ContainsKey(course))
//             _enrollments[course] = new List<TStudent>();

//         var students = _enrollments[course];

//         if (students.Count >= course.MaxCapacity)
//         {
//             reason = "Course is already at maximum capacity.";
//             return false;
//         }

//         if (students.Any(s => s.StudentId == student.StudentId))
//         {
//             reason = "Student is already enrolled in this course.";
//             return false;
//         }

//         if (course is LabCourse lab && student.Semester < lab.RequiredSemester)
//         {
//             reason = $"Prerequisite not met. Required semester: {lab.RequiredSemester}.";
//             return false;
//         }

//         students.Add(student);
//         reason = "Enrollment successful.";
//         return true;
//     }

//     public IReadOnlyList<TStudent> GetEnrolledStudents(TCourse course)
//     {
//         if (course == null) throw new ArgumentNullException(nameof(course));

//         return _enrollments.TryGetValue(course, out var students)
//             ? students.AsReadOnly()
//             : Array.Empty<TStudent>();
//     }

//     public IEnumerable<TCourse> GetStudentCourses(TStudent student)
//     {
//         if (student == null) throw new ArgumentNullException(nameof(student));

//         return _enrollments
//             .Where(kv => kv.Value.Any(s => s.StudentId == student.StudentId))
//             .Select(kv => kv.Key);
//     }

//     public int CalculateStudentWorkload(TStudent student)
//     {
//         if (student == null) throw new ArgumentNullException(nameof(student));

//         return GetStudentCourses(student).Sum(c => c.Credits);
//     }

//     public bool IsEnrolled(TStudent student, TCourse course)
//     {
//         return _enrollments.TryGetValue(course, out var students) &&
//                students.Any(s => s.StudentId == student.StudentId);
//     }
// }


// public class EngineeringStudent : IStudent
// {
//     public int StudentId { get; set; }
//     public string Name { get; set; }
//     public int Semester { get; set; }
//     public string Specialization { get; set; }
// }

// public class LabCourse : ICourse
// {
//     public string CourseCode { get; set; }
//     public string Title { get; set; }
//     public int MaxCapacity { get; set; }
//     public int Credits { get; set; }

//     public string LabEquipment { get; set; }
//     public int RequiredSemester { get; set; }
// }


// public class GradeBook<TStudent, TCourse>
//     where TStudent : IStudent
//     where TCourse : ICourse
// {
//     private readonly Dictionary<(TStudent, TCourse), double> _grades = new();
//     private readonly EnrollmentSystem<TStudent, TCourse> _enrollmentSystem;

//     public GradeBook(EnrollmentSystem<TStudent, TCourse> enrollmentSystem)
//     {
//         _enrollmentSystem = enrollmentSystem ?? throw new ArgumentNullException(nameof(enrollmentSystem));
//     }

//     public void AddGrade(TStudent student, TCourse course, double grade)
//     {
//         if (student == null || course == null)
//             throw new ArgumentNullException("Student or course cannot be null.");

//         if (grade < 0 || grade > 100)
//             throw new ArgumentOutOfRangeException(nameof(grade), "Grade must be between 0 and 100.");

//         if (!_enrollmentSystem.IsEnrolled(student, course))
//             throw new InvalidOperationException("Student is not enrolled in this course.");

//         _grades[(student, course)] = grade;
//     }

//     public double? CalculateGPA(TStudent student)
//     {
//         var entries = _grades
//             .Where(kv => kv.Key.Item1.StudentId == student.StudentId)
//             .Select(kv => new { Grade = kv.Value, Course = kv.Key.Item2 })
//             .ToList();

//         if (!entries.Any()) return null;

//         double totalWeighted = entries.Sum(e => e.Grade * e.Course.Credits);
//         int totalCredits = entries.Sum(e => e.Course.Credits)
