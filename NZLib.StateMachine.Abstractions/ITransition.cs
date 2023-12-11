namespace NZLib.StateMachine.Abstractions;

public interface ITransition<T>
{
	T Id { get; }

	T ToStateId { get; }

	T FromStateId { get; }

	T StateMachineId { get; }

	bool IsActive { get; }

	bool IsSystemic { get; set; }

	bool IsCommentRequired { get; }

	string Icon { get; }

	string Color { get; }

	string ActionTitle { get; }

	string Description { get; }
}
