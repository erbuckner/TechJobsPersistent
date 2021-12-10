--Part 1
SELECT column_name,data_type
FROM information_schema.columns
WHERE table_schema = 'techjobs' and table_name = 'Jobs';

--Part 2
SELECT Name
FROM Employers
WHERE (Location = 'St. Louis');

--Part 3
SELECT DISTINCT Skills.Name, Skills.Description
FROM JobSkills
RIGHT JOIN Skills
ON JobSkills.SkillId = Skills.Id
WHERE JobSkills.SkillId IS NOT NULL
ORDER BY Skills.Name ASC
