using System.Runtime.CompilerServices;

namespace PatternsLab.Problems.Prototype;



public static class EnemyRegistry
{
    private static readonly Dictionary<string, Enemy> _enemyRegistry = new();

    public static Enemy GetEnemy(string name)
    {
        if (!_enemyRegistry.TryGetValue(name, out var enemy))
        {
            throw new ArgumentException(
                "Enemy with this name was not found.",
                nameof(name));
        }

        return enemy.Clone();
    }

    public static void AddEnemy(Enemy enemy)
    {
        if (_enemyRegistry.ContainsKey(enemy.Name))
            return;

        _enemyRegistry.Add(enemy.Name, enemy);
    }
}

public class Weapon
{
    public string Name { get; set; } = string.Empty;
    public int Damage { get; set; }


    public Weapon Clone()
      => new Weapon { Name = this.Name, Damage = this.Damage };
}

public abstract class Enemy
{
    private string _modelData;

    public string Name { get; set; } = string.Empty;
    public int Health { get; set; }
    public Weapon Weapon { get; set; } = null!;
    public List<string> Abilities { get; set; } = new();
    public string ModelId => _modelData;

    protected Enemy(string modelData)
        => _modelData = modelData;

    protected Enemy()
    {
        Console.WriteLine("   ...loading 3D model (slow)...");
        Thread.Sleep(500);
        _modelData = "MODEL_" + Guid.NewGuid().ToString("N")[..6];
    }

    public abstract Enemy Clone();
}

public class Orc : Enemy
{
    private Orc(string modelData) : base(modelData) { }
    public Orc()
    {
        Name = "Orc";
        Health = 100;
        Weapon = new Weapon { Name = "Axe", Damage = 25 };
        Abilities.Add("Rage");
    }

    public override Enemy Clone()
    {
        Orc orcCopy = new Orc(this.ModelId);
        orcCopy.Name = this.Name;
        orcCopy.Health = this.Health;
        orcCopy.Weapon = this.Weapon.Clone();
        orcCopy.Abilities = new List<string>(this.Abilities);

        return orcCopy;
    }
}

public class Elf : Enemy
{
    private Elf(string modelData) : base(modelData) { }
    public Elf()
    {
        Name = "Elf";
        Health = 70;
        Weapon = new Weapon { Name = "Bow", Damage = 18 };
        Abilities.Add("Stealth");
    }

    public override Enemy Clone()
    {
        Elf elfCopy = new Elf(this.ModelId);
        elfCopy.Name =  this.Name;
        elfCopy.Health = this.Health;
        elfCopy.Weapon = this.Weapon.Clone();
        elfCopy.Abilities = new List<string>(this.Abilities);

        return elfCopy;
    }
}
