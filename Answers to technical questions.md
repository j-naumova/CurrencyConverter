## Answers to technical questions  

**1. How long did you spend on the coding assignment? What would you add to your solution if you had more time? If you didn't spend much time on the coding assignment then use this as an opportunity to explain what you would add.**

It was silly of me to take any tasks over Christmas and New Year period, so I've spent only a couple of days, maybe 6 hours of net time on the first free weekend with my laptop available, but tried not to be in any rush, so I've resorted to having only API with basic test coverage and simple validation/exception handling.

If it was part of some production, I would add:

0. I wouldn't keep the key in appsettings, it should be either in the parameter store or substituted while deployed
1. Front-end. Swagger is good, but the client opens possibilities for FE validation.
2. Authentication/authorization if required
3. Logging


**2. What was the most useful feature that was added to the latest version of your language of choice? Please include a snippet of code that shows how you've used it.**

From an aesthetic perspective, the number of positive emotions produced, and usage frequency that would be a file-scoped namespace declaration. It saves so much space and makes files look better.

You can find examples in the test task, it is trivial:

```
using CurrencyConverter.Services;
```

Also, I love that Records can be structs now, because it agrees with their usage as immutable value objects. Finding the example could be tricky, but I've used Records for coordinates in another test task:

```
public record struct Position(int X, int Y){}
```

**3. How would you track down a performance issue in production? Have you ever had to do this?**

- Logs of every possible kind to notice and locate the issue (Kibana logs, Grafana boards, alerts if something is suspicious)
- Local memory analysis in case of memory leak
- Local CPU load analysis in case of CPU issues
- Going through query plans and DB stats to understand which queries take so long and what exactly happens

Yes, I have some experience mostly with memory issues, but sometimes it was needed to optimize complex stored procedures too to lessen the load on the important database.

**4. What was the latest technical book you have read or tech conference you have been to? What did you learn?**

I was re-reading "SOLID Principles of Object-Oriented and Agile Design" by Robert C. Martin mostly refreshing my memory before interviews, but also with the hope to find something to use in refactoring my current project (it got more dependency inversion and factories right after that).

In terms of something new, I recently went through a DDD introduction course on Pluralsight by Steeve Smith and Julie Lerman and an article by David Laribee on the same topic — [Best Practice - An Introduction To Domain-Driven Design](https://learn.microsoft.com/en-us/archive/msdn-magazine/2009/february/best-practice-an-introduction-to-domain-driven-design). 

It is interesting how DDD is connected to philosophical concepts and is simply natural for human understanding but looks strange for programmers who are not used to it (me included). Going through the course and the article helped me to broaden my perspective on requirements gathering, and I even tried event storming for one of the bloated services (creating Jira cards for the start of its decomposition tomorrow btw!).

**5. What do you think about this technical assessment?**

I like that you explicitly ask to explain technical task assumptions and limitations. The task itself could be more complex in terms of design, but in my understanding, it checks whether the candidate is familiar with the basic concepts, so it is not too hard and not trivial.


**6. Please, describe yourself using JSON.**
```
{
  "firstName": "Julia",
  "lastName": "Naumova",
  "dateOfBirth": "1993-10-26",
  "location": {
    "country": "Estonia",
    "city": "Tallinn"
  },
  "languages": [
    {
      "language": "Russian",
      "proficiency": "Native"
    },
    {
      "language": "English",
      "proficiency": "Full Professional"
    }
  ],
  "mainSkills": [
    "C#",
    ".NET",
    "OOP",
    "MS SQL",
    "Agile"
  ],
  "interests": [
    "Books",
    "Cats",
    "Post-punk",
    "Small interesting things",
    "People being kind and creative",
    "Absurdity of everything",
    "Beading"
  ],
  "education": [
    {
      "school": "Saint Petersburg State University",
      "degree": "Bachelor's",
      "major": "Fundamental informatics and information technologies"
    },
    {
      "school": "Saint Petersburg State University",
      "degree": "Master's",
      "major": "Software and Administration of Information Systems"
    }
  ],
  "workExperience": [
    {
      "company": "Linnworks",
      "position": "Fullstack Developer/Team Lead"      
    },
    {
      "company": "HiQo Solutions",
      "position": ".NET Developer"      
    },
    {
      "company": "Santorin LLC",
      "position": ".NET Developer"      
    }
  ]
}
```
