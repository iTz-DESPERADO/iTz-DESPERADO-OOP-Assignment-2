using System.Diagnostics;
using PatternsLab.Problems.Builder;
using PatternsLab.Problems.Prototype;
using PatternsLab.Problems.Singleton;

Console.WriteLine("=== SINGLETON: AFTER ===\n");

var db = new DatabaseService();
var ui = new UiService();

Console.WriteLine();

db.Config.Theme = "Dark";
Console.WriteLine("Admin changed theme to Dark.");

ui.Render();

Console.WriteLine($"\nSame config object? {ReferenceEquals(db.Config, ui.Config)}");
Console.WriteLine($"Times config was loaded from disk: {AppConfig.LoadCount}");

Console.WriteLine("\n=== PROTOTYPE: AFTER ===\n");

Enemy prototype = new Orc();

var sw = Stopwatch.StartNew();

var army = new List<Enemy>();

for (int i = 1; i <= 5; i++)
{
    Enemy orc = prototype.Clone();
    orc.Name = $"Orc-{i}";
    army.Add(orc);
}

sw.Stop();

Console.WriteLine($"Cloned 5 orcs in {sw.ElapsedMilliseconds} ms\n");

Enemy original = new Orc();
original.Name = "Boss Orc";
Enemy copy = original.Clone();


Console.WriteLine($"\nOriginal model id: {original.ModelId}");
Console.WriteLine($"Copy model id:     {copy.ModelId}");

copy.Weapon.Damage = 999;

Console.WriteLine("\nWe changed the COPY's weapon damage to 999.");
Console.WriteLine($"Original's weapon damage: {original.Weapon.Damage}");

copy.Abilities.Add("Fire Breath");

Console.WriteLine("\nWe added Fire Breath to the COPY.");
Console.WriteLine($"Original abilities: {string.Join(", ", original.Abilities)}");



Console.WriteLine("\n=== PROTOTYPE REGISTRY TEST ===\n");

EnemyRegistry.AddEnemy(new Orc());
EnemyRegistry.AddEnemy(new Elf());

sw = Stopwatch.StartNew();

Enemy orc1 = EnemyRegistry.GetEnemy("Orc");
Enemy orc2 = EnemyRegistry.GetEnemy("Orc");

sw.Stop();

Console.WriteLine($"Retrieved 2 Orc clones in {sw.ElapsedMilliseconds} ms");

Console.WriteLine($"\nOrc 1 ModelId: {orc1.ModelId}");
Console.WriteLine($"Orc 2 ModelId: {orc2.ModelId}");

Console.WriteLine(
    $"Same ModelId: {orc1.ModelId == orc2.ModelId}");

Console.WriteLine(
    $"Same object reference: {ReferenceEquals(orc1, orc2)}");

orc1.Weapon.Damage = 500;
orc1.Abilities.Add("Ground Smash");

Console.WriteLine("\nAfter changing Orc 1:");

Console.WriteLine($"Orc 1 damage: {orc1.Weapon.Damage}");
Console.WriteLine($"Orc 2 damage: {orc2.Weapon.Damage}");

Console.WriteLine(
    $"Orc 1 abilities: {string.Join(", ", orc1.Abilities)}");

Console.WriteLine(
    $"Orc 2 abilities: {string.Join(", ", orc2.Abilities)}");

Enemy elf = EnemyRegistry.GetEnemy("Elf");

Console.WriteLine($"\nElf: {elf.Name}");
Console.WriteLine($"Elf ModelId: {elf.ModelId}");
Console.WriteLine($"Elf Weapon: {elf.Weapon.Name}");



Console.WriteLine("\n=== BUILDER: AFTER ===\n");
Console.WriteLine(RegistrationCallSites.CreateLiveStudent());
Console.WriteLine(RegistrationCallSites.CreateVideosOnly());

Console.WriteLine("\n=== BUILDER VALIDATION TEST ===\n");

try
{
    new CourseRegistrationBuilder(
        "test@mail.com",
        "SEF-101",
        "LiveGroup")
        .Build();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

try
{
    new CourseRegistrationBuilder(
        "test@mail.com",
        "SEF-101",
        "VideosOnly")
        .WithGroupCode("G1")
        .Build();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
