using System;
using System.Collections.Generic;

namespace DomainDataGenerator
{
    /// <summary>
    /// Generates random names for testing purposes.
    /// </summary>
    public static class NameGenerator
    {
        private static readonly Random Random = new Random();

        private static List<string> FirstNames = new List<string> {
            "John", "Jane", "Alex", "Emily", "Chris", "Katie", "Michael", "Sarah", "David", "Laura",
            "Aaron", "Abigail", "Adam", "Aiden", "Alan", "Albert", "Alec", "Alexa", "Alice", "Alicia",
            "Allison", "Alyssa", "Amanda", "Amber", "Amy", "Andrea", "Andrew", "Angela", "Anna", "Anthony",
            "Ashley", "Austin", "Barbara", "Benjamin", "Beth", "Blake", "Brandon", "Brenda", "Brian", "Brianna",
            "Brittany", "Brooke", "Bruce", "Bryan", "Caleb", "Cameron", "Carl", "Carla", "Carlos", "Carmen",
            "Carol", "Caroline", "Carolyn", "Carter", "Cassandra", "Catherine", "Cecilia", "Chad", "Charles", "Charlotte",
            "Cheryl", "Chloe", "Christian", "Christina", "Christine", "Christopher", "Claire", "Clara", "Clarence", "Claudia",
            "Clayton", "Clifford", "Colin", "Connor", "Courtney", "Craig", "Crystal", "Curtis", "Cynthia", "Daisy",
            "Dakota", "Dale", "Dana", "Daniel", "Danielle", "Daphne", "Darlene", "Darren", "Darryl", "Dawn",
            "Dean", "Deanna", "Deborah", "Debra", "Denise", "Dennis", "Derek", "Derrick", "Diana", "Diane",
            "Dominic", "Donna", "Doris", "Dorothy", "Douglas", "Dylan", "Eddie", "Edgar", "Edith", "Edward",
            "Elaine", "Eleanor", "Elena", "Eli", "Elijah", "Elizabeth", "Ella", "Ellen", "Ellie", "Emily",
            "Emma", "Eric", "Erica", "Erik", "Erin", "Ethan", "Eugene", "Eva", "Evan", "Evelyn",
            "Faith", "Felicia", "Felix", "Fiona", "Florence", "Frances", "Francis", "Frank", "Gabriel", "Gail",
            "Gavin", "Gene", "Geoffrey", "George", "Georgia", "Gerald", "Geraldine", "Gina", "Glen", "Glenda",
            "Glenn", "Gloria", "Grace", "Grant", "Greg", "Gregory", "Gwen", "Hailey", "Hannah", "Harold",
            "Harry", "Hayden", "Heather", "Heidi", "Helen", "Henry", "Holly", "Howard", "Hunter", "Ian",
            "Irene", "Isaac", "Isabel", "Isabella", "Jack", "Jackie", "Jacob", "Jacqueline", "Jade", "Jake",
            "James", "Jamie", "Janet", "Janice", "Jared", "Jason", "Jean", "Jeff", "Jeffrey", "Jenna",
            "Jennifer", "Jeremy", "Jerry", "Jesse", "Jessica", "Jill", "Joan", "Joanna", "Joe", "Joel",
            "Johnathan", "Johnny", "Jordan", "Jose", "Joseph", "Joshua", "Joy", "Joyce", "Juan", "Judith",
            "Judy", "Julia", "Julian", "Julie", "June", "Justin", "Kara", "Karen", "Katherine", "Kathleen",
            "Kathryn", "Kathy", "Katie", "Kayla", "Keith", "Kelly", "Kelsey", "Kenneth", "Kevin", "Kim",
            "Kimberly", "Kristen", "Kristin", "Kyle", "Lance", "Larry", "Laura", "Lauren", "Laurie", "Lawrence",
            "Leah", "Lee", "Leonard", "Leslie", "Lillian", "Linda", "Lindsay", "Lindsey", "Lisa", "Logan",
            "Lois", "Lori", "Louis", "Lucas", "Lucia", "Lucy", "Luis", "Luke", "Lydia", "Madeline",
            "Madison", "Maggie", "Manuel", "Marc", "Marcia", "Margaret", "Maria", "Marie", "Marilyn", "Marion",
            "Marissa", "Mark", "Marlene", "Marsha", "Marshall", "Martha", "Martin", "Marvin", "Mary", "Mason",
            "Matthew", "Maureen", "Megan", "Melanie", "Melinda", "Melissa", "Melody", "Meredith", "Michael", "Michele",
            "Michelle", "Miguel", "Mildred", "Molly", "Monica", "Morgan", "Nancy", "Natalie", "Nathan", "Nathaniel",
            "Neil", "Nicholas", "Nicole", "Noah", "Norma", "Olivia", "Oscar", "Pamela", "Patricia", "Patrick",
            "Paul", "Paula", "Pauline", "Peggy", "Penny", "Peter", "Philip", "Phillip", "Phyllis", "Preston",
            "Rachel", "Ralph", "Randy", "Ray", "Raymond", "Rebecca", "Regina", "Renee", "Rhonda", "Richard",
            "Rick", "Rita", "Roberta", "Robert", "Robin", "Rodney", "Roger", "Ronald", "Rosa", "Rose",
            "Rosemary", "Ross", "Roy", "Ruby", "Russell", "Ruth", "Ryan", "Sally", "Samantha", "Samuel",
            "Sandra", "Sara", "Sarah", "Scott", "Sean", "Shane", "Shannon", "Sharon", "Sheila", "Shelby",
            "Shelley", "Sherri", "Sherry", "Shirley", "Sidney", "Sierra", "Simon", "Sonia", "Sophie", "Spencer",
            "Stacy", "Stanley", "Stephanie", "Stephen", "Steve", "Steven", "Stuart", "Sue", "Summer", "Susan",
            "Suzanne", "Sydney", "Sylvia", "Tamara", "Tammy", "Tanya", "Tara", "Taylor", "Ted", "Teresa",
            "Terri", "Terry", "Theodore", "Theresa", "Thomas", "Tiffany", "Timothy", "Tina", "Todd", "Tom",
            "Toni", "Tony", "Tracy", "Travis", "Trent", "Trevor", "Tricia", "Tristan", "Troy", "Tyler",
            "Valerie", "Vanessa", "Vera", "Veronica", "Vicki", "Vickie", "Victor", "Victoria", "Vincent", "Violet",
            "Virginia", "Vivian", "Wade", "Walter", "Wanda", "Warren", "Wayne", "Wendy", "Wesley", "Whitney",
            "Willard", "William", "Willie", "Wyatt", "Yolanda", "Yvonne", "Zachary", "Zoe"
        };

        private static List<string> LastNames = new List<string> {
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Martinez", "Wilson",
            "Anderson", "Taylor", "Thomas", "Hernandez", "Moore", "Martin", "Jackson", "Thompson", "White", "Lopez",
            "Lee", "Gonzalez", "Harris", "Clark", "Lewis", "Robinson", "Walker", "Perez", "Hall", "Young",
            "Allen", "Sanchez", "Wright", "King", "Scott", "Green", "Baker", "Adams", "Nelson", "Hill",
            "Ramirez", "Campbell", "Mitchell", "Roberts", "Carter", "Phillips", "Evans", "Turner", "Torres", "Parker",
            "Collins", "Edwards", "Stewart", "Flores", "Morris", "Nguyen", "Murphy", "Rivera", "Cook", "Rogers",
            "Morgan", "Peterson", "Cooper", "Reed", "Bailey", "Bell", "Gomez", "Kelly", "Howard", "Ward",
            "Cox", "Diaz", "Richardson", "Wood", "Watson", "Brooks", "Bennett", "Gray", "James", "Reyes",
            "Cruz", "Hughes", "Price", "Myers", "Long", "Foster", "Sanders", "Ross", "Morales", "Powell",
            "Sullivan", "Russell", "Ortiz", "Jenkins", "Gutierrez", "Perry", "Butler", "Barnes", "Fisher", "Henderson",
            "Coleman", "Simmons", "Patterson", "Jordan", "Reynolds", "Hamilton", "Graham", "Kim", "Gonzales", "Alexander",
            "Ramos", "Wallace", "Griffin", "West", "Cole", "Hayes", "Chavez", "Gibson", "Bryant", "Ellis",
            "Stevens", "Murray", "Ford", "Marshall", "Owens", "Mcdonald", "Harrison", "Ruiz", "Kennedy", "Wells",
            "Alvarez", "Woods", "Mendoza", "Castillo", "Olson", "Webb", "Washington", "Tucker", "Freeman", "Burns",
            "Henry", "Vasquez", "Snyder", "Simpson", "Crawford", "Jimenez", "Porter", "Mason", "Shaw", "Gordon",
            "Wagner", "Hunter", "Romero", "Hicks", "Dixon", "Hunt", "Palmer", "Robertson", "Black", "Holmes",
            "Stone", "Meyer", "Boyd", "Mills", "Warren", "Fox", "Rose", "Rice", "Moreno", "Schmidt",
            "Patel", "Ferguson", "Nichols", "Herrera", "Medina", "Ryan", "Fernandez", "Weaver", "Daniels", "Stephens",
            "Gardner", "Payne", "Kelley", "Dunn", "Pierce", "Arnold", "Tran", "Spencer", "Peters", "Hawkins",
            "Grant", "Hansen", "Castro", "Hoffman", "Hart", "Elliott", "Cunningham", "Knight", "Bradley", "Carroll",
            "Hudson", "Duncan", "Armstrong", "Berry", "Andrews", "Johnston", "Ray", "Lane", "Riley", "Carson",
            "Perkins", "Aguilar", "Silva", "Richards", "Willis", "Matthews", "Chapman", "Lawrence", "Garza", "Vargas",
            "Watkins", "Wheeler", "Larson", "Carlson", "Harper", "George", "Greene", "Burke", "Guzman", "Morrison",
            "Munoz", "Jacobs", "Obrien", "Lawson", "Franklin", "Lynch", "Bishop", "Carr", "Salazar", "Austin",
            "Mendez", "Gilbert", "Jensen", "Williamson", "Montgomery", "Harvey", "Oliver", "Howell", "Dean", "Hanson",
            "Weber", "Garrett", "Sims", "Burton", "Fuller", "Soto", "Mccoy", "Welch", "Chen", "Schultz",
            "Walters", "Reid", "Fields", "Walsh", "Little", "Fowler", "Bowman", "Davidson", "May", "Day",
            "Schneider", "Newman", "Brewer", "Lucas", "Holland", "Wong", "Banks", "Santos", "Curtis", "Pearson",
            "Delgado", "Valdez", "Pena", "Rios", "Douglas", "Sandoval", "Barrett", "Hopkins", "Keller", "Guerrero",
            "Stanley", "Bates", "Alvarado", "Beck", "Ortega", "Wade", "Estrada", "Contreras", "Barnett", "Caldwell",
            "Santiago", "Lambert", "Powers", "Chambers", "Nunez", "Craig", "Leonard", "Lowe", "Rhodes", "Byrd",
            "Gregory", "Shelton", "Frazier", "Becker", "Maldonado", "Fleming", "Vega", "Sutton", "Cohen", "Jennings",
            "Parks", "Mcdaniel", "Watts", "Barker", "Norris", "Vaughn", "Vazquez", "Holt", "Schwartz", "Steele",
            "Benson", "Neal", "Dominguez", "Horton", "Terry", "Wolfe", "Hale", "Lyons", "Graves", "Haynes",
            "Miles", "Park", "Warner", "Padilla", "Bush", "Thornton", "Mccarthy", "Mann", "Zimmerman", "Erickson"
        };

        public static string GenerateFirstName()
        {
            return FirstNames[Random.Next(FirstNames.Count)];
            
            
        }

        public static string GenerateLastName()
        {
            return LastNames[Random.Next(LastNames.Count)];
        }
    }
}