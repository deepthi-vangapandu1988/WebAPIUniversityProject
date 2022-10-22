using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;
using System.Xml.Linq;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LINQTOXMLController : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public async Task<ActionResult<APIResponse>> GetStudentsAsync()
        {
            try
            {
                var filename = "StudentInfo.xml";
                var currentDirectory = Directory.GetCurrentDirectory();
                var studentInfoFilepath = Path.Combine(currentDirectory, filename);

                XElement studentInfo = XElement.Load(studentInfoFilepath);

                IList<string> studentClasses = (from item in studentInfo.Descendants("Student")
                                                select (string)item.Attribute("class")).ToList();

                IList<string> studentClassesLambda = studentInfo.Descendants("Student").Select(x => (string)x.Attribute("class")).ToList();

                IList<string> distinctStudentClasses = (from item in studentInfo.Descendants("Student")
                                                        select (string)item.Attribute("class")).Distinct().ToList();

                IList<string> distinctStudentClassesLambda = studentInfo.Descendants("Student").Select(x => (string)x.Attribute("class")).Distinct().ToList();

                IList<string> studentsFromHyd = (from item in studentInfo.Descendants("Student")
                                                 where (string)item.Element("Address") == "Hyd"
                                                 select (string)item.Element("Name")).ToList();


                IList<string> studentsFromHydLambda = studentInfo.Descendants("Student")
                                                        .Where(x => (string)x.Element("Address") == "Hyd")
                                                        .Select(x => (string)x.Element("Name")).ToList();

                int getSumOfStudentMarks = studentInfo.Descendants("Student")
                                                        .Select(x => (int)x.Element("Marks")).Sum();



                IList<string> studentsOrderByNameLambda = studentInfo.Descendants("Student")
                                                        .Select(x => (string)x.Element("Name")).OrderBy(n => n).ToList();

                IList<string> studentsOrderByDescendingNameLambda = studentInfo.Descendants("Student")
                                                        .Select(x => (string)x.Element("Name")).OrderByDescending(n => n).ToList();

                int newStudentid = studentsOrderByNameLambda.Count + 1;

                studentInfo.Add(new XElement("Student",
                    new XAttribute("class", 9),
                    new XElement("Id", newStudentid),
                    new XElement("Name", "Student" + newStudentid),
                    new XElement("Email", "St" + newStudentid + "@gmail.com"),
                    new XElement("Address", "Banglore"),
                    new XElement("Marks", 20 * newStudentid)));

                studentInfo.Save(studentInfoFilepath, SaveOptions.DisableFormatting);

                var studentToUpdate = studentInfo.Elements("Student").Single(n => (int)n.Element("Id") == 2);
                studentToUpdate.Element("Name").Value = "New Student name";

                studentInfo.Save(studentInfoFilepath, SaveOptions.DisableFormatting);

                var studentToDelete = studentInfo.Elements("Student").Single(n => (int)n.Element("Id") == 2);
                studentToDelete.Remove();

                studentInfo.Save(studentInfoFilepath, SaveOptions.DisableFormatting);

                return new APIResponse(System.Net.HttpStatusCode.OK, true, null);
            }
            catch (Exception ex)
            {
                return new APIResponse(System.Net.HttpStatusCode.InternalServerError, false, ex.Message);
            }
            return null;
        }
    }
}
