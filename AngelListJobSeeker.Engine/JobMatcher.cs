using System.Linq;

using Newtonsoft.Json;

using AngelListJobSeeker.Engine.Models;

namespace AngelListJobSeeker.Engine
{
    public class JobMatcher
    {
        public static IQueryable GetTopTenCompanies(Profile candidateProfile)
        {
            var profile = candidateProfile;
            var candidateLocation = ((string)((dynamic)(profile.home_address)).city).ToLower().Trim();
            var roles = profile.experience.Select(x => ((string) ((dynamic) x).role).ToLower().Trim()).Union(
                profile.projects.Select(p => ((string) ((dynamic) p).role).ToLower().Trim())).ToList();
            var jobs = JsonConvert.DeserializeObject<Job[]>(ApiWrapper.GetJobListing())
                                  .Where(j => j.startup.community_profile == false &&
                                              profile.looking_for.Contains(j.job_type));

            foreach (var job in jobs)
            {
                var skillPoints = job.tags.Where(t => t.tag_type.Trim() == "SkillTag")
                                     .Select(t => t.display_name.ToLower().Trim())
                                     .Intersect(profile.skills.Select(s => s.ToLower().Trim())).Count();

                var rolePoints = job.tags.Where(t => t.tag_type.Trim() == "RoleTag").
                                     Select(t => t.display_name.ToLower().Trim())
                                    .Intersect(roles).Count();
                
                var locationPoints = -2;
                var jobLocation = job.tags.FirstOrDefault(t => t.tag_type.Trim() == "LocationTag");

                if (jobLocation != null)
                    locationPoints = candidateLocation == jobLocation.display_name.ToLower().Trim()
                                         ? 1
                                         : (profile.willing_to_relocate ? 0 : -1);

                job.job_rank = skillPoints + rolePoints + locationPoints;
            }

            var companies = jobs.Where(j => j.job_rank > 0)
                       .OrderByDescending(j => j.job_rank)
                       .GroupBy(j => j.startup.id)
                       .OrderByDescending(s => s.First().job_rank)
                       .ThenByDescending(s => s.First().startup.quality)
                       .Take(10).AsQueryable();

            return companies;
        }
    }
}
