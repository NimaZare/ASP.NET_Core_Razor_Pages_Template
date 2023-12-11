using System.ComponentModel.DataAnnotations;

namespace Domain.Seedwork;

public abstract class Entity : NZLib.Seedwork.Abstractions.IEntity<Guid>
{
	public Entity()
	{
		Ordering = 10_000;
		Id = Guid.NewGuid();
		InsertDateTime = NZLib.Utility.Now;
	}


	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Id))]

	[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated
		(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
	public Guid Id { get; protected set; }


	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Ordering))]

	[Range(minimum: Constants.MinValue.Ordering, maximum: Constants.MaxValue.Ordering,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Range))]
	public int Ordering { get; set; }


	[Display(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.InsertDateTime))]

	[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated
		(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
	public DateTime InsertDateTime { get; private set; }

}
