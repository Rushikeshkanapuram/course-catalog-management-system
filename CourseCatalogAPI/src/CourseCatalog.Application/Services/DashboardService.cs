using CourseCatalog.Application.DTOs.Dashboard;
using CourseCatalog.Application.Interfaces.Repositories;
using CourseCatalog.Application.Interfaces.Services;

namespace CourseCatalog.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly IUserRepository _userRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;

    public DashboardService(
        IUserRepository userRepository,
        ICourseRepository courseRepository,
        IEnrollmentRepository enrollmentRepository)
    {
        _userRepository = userRepository;
        _courseRepository = courseRepository;
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<AdminDashboardDto> GetAdminDashboardAsync()
    {
        return new AdminDashboardDto
        {
            TotalUsers = await _userRepository.GetTotalUsersAsync(),
            TotalStudents = await _userRepository.GetTotalStudentsAsync(),
            TotalInstructors = await _userRepository.GetTotalInstructorsAsync(),
            TotalCourses = await _courseRepository.GetTotalCoursesAsync(),
            TotalEnrollments = await _enrollmentRepository.GetTotalEnrollmentsAsync()
        };
    }

    public async Task<InstructorDashboardDto> GetInstructorDashboardAsync(int instructorId)
    {
        return new InstructorDashboardDto
        {
            MyCourses = await _courseRepository.GetInstructorCourseCountAsync(instructorId),
            TotalStudents = await _enrollmentRepository.GetInstructorStudentCountAsync(instructorId),
            TotalEnrollments = await _enrollmentRepository.GetInstructorEnrollmentCountAsync(instructorId)
        };
    }

    public async Task<StudentDashboardDto> GetStudentDashboardAsync(int studentId)
    {
        var enrolledCourses =
            await _enrollmentRepository.GetStudentEnrollmentCountAsync(studentId);

        var totalCourses =
            await _courseRepository.GetTotalCoursesAsync();

        return new StudentDashboardDto
        {
            EnrolledCourses = enrolledCourses,
            AvailableCourses = totalCourses - enrolledCourses
        };
    }
}