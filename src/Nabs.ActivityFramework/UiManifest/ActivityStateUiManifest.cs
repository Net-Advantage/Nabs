namespace Nabs.ActivityFramework.UiManifest;

public abstract class ActivityStateUiManifest<T> : IActivityStateUiManifestTitle<T>, IActivityStateUiManifestProperty<T>
	where T : class, IActivityState
{
	private readonly UiManifestTitle _manifestTitle = new();
	private readonly Dictionary<string, UiManifestItem> _manifestItems = [];
	private UiManifestItem _currentManifestItem = default!;

	protected IActivityStateUiManifestTitle<T> TitleFor(string title)
	{
		_manifestTitle.Title = title;
		return this;
	}

	public IActivityStateUiManifestTitle<T> WithDescription(string description)
	{
		_manifestTitle.Description = description;
		return this;
	}

	protected IActivityStateUiManifestProperty<T> PropertyFor<TProperty>(Expression<Func<T, TProperty>> expression)
	{
		var member = expression.Body as MemberExpression;
		var propertyName = member!.Member.Name;
		_currentManifestItem = new UiManifestItem();
		_manifestItems[propertyName] = _currentManifestItem;
		return this;
	}

	public IActivityStateUiManifestProperty<T> WithSequence(int sequence)
	{
		_currentManifestItem.Sequence = sequence;
		return this;
	}

	public IActivityStateUiManifestProperty<T> WithLabel(string label)
	{
		_currentManifestItem.Label = label;
		return this;
	}

	public IActivityStateUiManifestProperty<T> WithPlaceholder(string placeholder)
	{
		_currentManifestItem.Placeholder = placeholder;
		return this;
	}

	public IActivityStateUiManifestProperty<T> WithHelpText(string helpText)
	{
		_currentManifestItem.HelpText = helpText;
		return this;
	}

	public IActivityStateUiManifestProperty<T> WithValidationRule(UiValidationRule rule, string message)
	{
		_currentManifestItem.ValidationRule = rule;
		_currentManifestItem.ValidationRuleMessage = message;
		return this;
	}

	public UiManifestResult Render()
	{
		return new UiManifestResult
		{
			Title = _manifestTitle,
			Items = new List<UiManifestItem>(_manifestItems.Values)
		};
	}
}
