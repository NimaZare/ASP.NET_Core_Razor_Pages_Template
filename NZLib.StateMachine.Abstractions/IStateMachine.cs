namespace NZLib.StateMachine.Abstractions;

public interface IStateMachine<T>
{
	T Id { get; }

	int Code { get; }

	string Factor { get; }

	bool IsValid { get; }

	bool IsActive { get; }

	string Title { get; }

	string Description { get; }

	string TargetEntityFieldName { get; }

	void Validate();

	void AddState(IState<T> state);

	void RemoveStateById(T stateId);

	void RemoveState(IState<T> state);

	void UpdateState(IState<T> state);

	void AddTransition(ITransition<T> transition);

	void RemoveTransitionById(T transitionId);

	void RemoveTransition(ITransition<T> transition);

	void UpdateTransition(ITransition<T> transition);

	IReadOnlyList<IState<T>> States { get; }

	IReadOnlyList<ITransition<T>> Transitions { get; }
}
