using BytecodeApi.Extensions;
using BytecodeApi.Mathematics;

namespace BytecodeApi.LanguageGenerator;

/// <summary>
/// Class that generates a random human name.
/// </summary>
public class NameGenerator : ILanguageStringGenerator
{
	private static readonly string[] DefaultMaleFirstNames;
	private static readonly string[] DefaultFemaleFirstNames;
	private static readonly string[] DefaultLastNames;
	/// <summary>
	/// Gets or sets an array of <see cref="string" /> values, each representing a male first name to randomly select from during name generation.
	/// <para>The default value is a set of 100 predefined names.</para>
	/// </summary>
	public string[] MaleFirstNames { get; set; }
	/// <summary>
	/// Gets or sets an array of <see cref="string" /> values, each representing a female first name to randomly select from during name generation.
	/// <para>The default value is a set of 100 predefined names.</para>
	/// </summary>
	public string[] FemaleFirstNames { get; set; }
	/// <summary>
	/// Gets or sets an array of <see cref="string" /> values, each representing a last name to randomly select from during name generation.
	/// <para>The default value is a set of 100 predefined names.</para>
	/// </summary>
	public string[] LastNames { get; set; }
	/// <summary>
	/// <see langword="true" /> to use male first names.
	/// </summary>
	public bool UseMaleFirstNames { get; set; }
	/// <summary>
	/// <see langword="true" /> to use female first names.
	/// </summary>
	public bool UseFemaleFirstNames { get; set; }
	/// <summary>
	/// Gets or sets the male salutation.
	/// <para>The default value is "Mr."</para>
	/// </summary>
	public string MaleSalutation { get; set; }
	/// <summary>
	/// Gets or sets the female salutation.
	/// <para>The default value is "Ms."</para>
	/// </summary>
	public string FemaleSalutation { get; set; }
	/// <summary>
	/// Gets or sets the format to use during name generation.
	/// <para>The default value is "{0} {1}"</para>
	/// <para>{0} is the first name.</para>
	/// <para>{1} is the last name.</para>
	/// <para>{2} is the salutation (excluded by default).</para>
	/// </summary>
	public string Format { get; set; }

	static NameGenerator()
	{
		DefaultMaleFirstNames = ["James", "Robert", "John", "Michael", "David", "William", "Richard", "Joseph", "Thomas", "Charles", "Christopher", "Daniel", "Matthew", "Anthony", "Mark", "Donald", "Steven", "Paul", "Andrew", "Joshua", "Kenneth", "Kevin", "Brian", "George", "Timothy", "Ronald", "Edward", "Jason", "Jeffrey", "Ryan", "Jacob", "Gary", "Nicholas", "Eric", "Jonathan", "Stephen", "Larry", "Justin", "Scott", "Brandon", "Benjamin", "Samuel", "Gregory", "Alexander", "Frank", "Patrick", "Raymond", "Jack", "Dennis", "Jerry", "Tyler", "Aaron", "Jose", "Adam", "Nathan", "Henry", "Douglas", "Zachary", "Peter", "Kyle", "Ethan", "Walter", "Noah", "Jeremy", "Christian", "Keith", "Roger", "Terry", "Gerald", "Harold", "Sean", "Austin", "Carl", "Arthur", "Lawrence", "Dylan", "Jesse", "Jordan", "Bryan", "Billy", "Joe", "Bruce", "Gabriel", "Logan", "Albert", "Willie", "Alan", "Juan", "Wayne", "Elijah", "Randy", "Roy", "Vincent", "Ralph", "Eugene", "Russell", "Bobby", "Mason", "Philip", "Louis"];
		DefaultFemaleFirstNames = ["Mary", "Patricia", "Jennifer", "Linda", "Elizabeth", "Barbara", "Susan", "Jessica", "Sarah", "Karen", "Lisa", "Nancy", "Betty", "Margaret", "Sandra", "Ashley", "Kimberly", "Emily", "Donna", "Michelle", "Carol", "Amanda", "Dorothy", "Melissa", "Deborah", "Stephanie", "Rebecca", "Sharon", "Laura", "Cynthia", "Kathleen", "Amy", "Angela", "Shirley", "Anna", "Brenda", "Pamela", "Emma", "Nicole", "Helen", "Samantha", "Katherine", "Christine", "Debra", "Rachel", "Carolyn", "Janet", "Catherine", "Maria", "Heather", "Diane", "Ruth", "Julie", "Olivia", "Joyce", "Virginia", "Victoria", "Kelly", "Lauren", "Christina", "Joan", "Evelyn", "Judith", "Megan", "Andrea", "Cheryl", "Hannah", "Jacqueline", "Martha", "Gloria", "Teresa", "Ann", "Sara", "Madison", "Frances", "Kathryn", "Janice", "Jean", "Abigail", "Alice", "Julia", "Judy", "Sophia", "Grace", "Denise", "Amber", "Doris", "Marilyn", "Danielle", "Beverly", "Isabella", "Theresa", "Diana", "Natalie", "Brittany", "Charlotte", "Marie", "Kayla", "Alexis", "Lori"];
		DefaultLastNames = ["Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez", "Hernandez", "Lopez", "Gonzales", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin", "Lee", "Perez", "Thompson", "White", "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson", "Walker", "Young", "Allen", "King", "Wright", "Scott", "Torres", "Nguyen", "Hill", "Flores", "Green", "Adams", "Nelson", "Baker", "Hall", "Rivera", "Campbell", "Mitchell", "Carter", "Roberts", "Gomez", "Phillips", "Evans", "Turner", "Diaz", "Parker", "Cruz", "Edwards", "Collins", "Reyes", "Stewart", "Morris", "Morales", "Murphy", "Cook", "Rogers", "Gutierrez", "Ortiz", "Morgan", "Cooper", "Peterson", "Bailey", "Reed", "Kelly", "Howard", "Ramos", "Kim", "Cox", "Ward", "Richardson", "Watson", "Brooks", "Chavez", "Wood", "James", "Bennet", "Gray", "Mendoza", "Ruiz", "Hughes", "Price", "Alvarez", "Castillo", "Sanders", "Patel", "Myers", "Long", "Ross", "Foster", "Jimenez"];
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="NameGenerator" /> class with default values.
	/// </summary>
	public NameGenerator()
	{
		MaleFirstNames = DefaultMaleFirstNames.ToArray();
		FemaleFirstNames = DefaultFemaleFirstNames.ToArray();
		LastNames = DefaultLastNames.ToArray();
		UseMaleFirstNames = true;
		UseFemaleFirstNames = true;
		MaleSalutation = "Mr.";
		FemaleSalutation = "Ms.";
		Format = "{0} {1}";
	}

	/// <summary>
	/// Generates a random human name.
	/// </summary>
	/// <returns>
	/// A new <see cref="string" /> with a randomly generated human name.
	/// </returns>
	public string Generate()
	{
		Check.ArgumentNull(MaleFirstNames);
		Check.ArgumentEx.ArrayElementsRequired(MaleFirstNames);
		Check.ArgumentEx.ArrayValuesNotNull(MaleFirstNames);
		Check.ArgumentEx.ArrayValuesNotStringEmpty(MaleFirstNames);
		Check.ArgumentNull(FemaleFirstNames);
		Check.ArgumentEx.ArrayElementsRequired(FemaleFirstNames);
		Check.ArgumentEx.ArrayValuesNotNull(FemaleFirstNames);
		Check.ArgumentEx.ArrayValuesNotStringEmpty(FemaleFirstNames);
		Check.ArgumentNull(LastNames);
		Check.ArgumentEx.ArrayElementsRequired(LastNames);
		Check.ArgumentEx.ArrayValuesNotNull(LastNames);
		Check.ArgumentEx.ArrayValuesNotStringEmpty(LastNames);
		Check.Argument(UseMaleFirstNames || UseFemaleFirstNames, null, $"Either '{nameof(UseMaleFirstNames)}' or '{nameof(UseFemaleFirstNames)}' must be true.");
		Check.ArgumentNull(Format);

		List<string[]> firstNameLists = [];
		if (UseMaleFirstNames) firstNameLists.Add(MaleFirstNames);
		if (UseFemaleFirstNames) firstNameLists.Add(FemaleFirstNames);

		string[] firstNames = Random.Shared.GetObject(firstNameLists);
		string salutation = firstNames == MaleFirstNames ? MaleSalutation : FemaleSalutation;

		return Format.FormatInvariant(Random.Shared.GetObject(firstNames), Random.Shared.GetObject(LastNames), salutation);
	}
}