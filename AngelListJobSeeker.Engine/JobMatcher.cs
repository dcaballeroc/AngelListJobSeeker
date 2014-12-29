using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using AngelListJobSeeker.Engine.Models;

namespace AngelListJobSeeker.Engine
{
    public class JobMatcher
    {
        public static void GetTopTenJobs(Profile candidateProfile)
        {
            var profile = candidateProfile;
            var jobs = JsonConvert.DeserializeObject<Job[]>(ApiWrapper.GetJobListing())
                                  .Where(j => j.startup.community_profile == false);

            foreach (var job in jobs)
            {
                var skillPoints = job.tags.Where(t => t.tag_type.Trim() == "SkillTag")
                                     .Select(t => t.display_name.ToLower().Trim())
                                     .Intersect(profile.skills.Select(s => s.ToLower().Trim())).Count();

                var roles = profile.experience.Select(x => ((string) ((dynamic) x).role).ToLower().Trim()).Union(
                    profile.projects.Select(p => ((string) ((dynamic) p).role).ToLower().Trim())).ToList();

                var rolePoints = job.tags.Where(t => t.tag_type.Trim() == "RoleTag").Select(t => t.display_name.ToLower().Trim()).Intersect(roles).Count();

                var candidateLocation = ((string) ((dynamic) (profile.home_address)).city).ToLower().Trim();
                var jobLocation = job.tags.First(t => t.tag_type.Trim() == "LocationTag").display_name.ToLower().Trim();
                var locationPoints = candidateLocation == jobLocation ? 1 : (profile.willing_to_relocate ? 0 : -1);


                int j = 0;
            }

            jobs = null;
        }
    }
}
