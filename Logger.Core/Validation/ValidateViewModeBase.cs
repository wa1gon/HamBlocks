namespace LoggerWPF.Core.Validation;

public class ValidateViewModeBase : ViewModelBase, INotifyDataErrorInfo
{
    private readonly Dictionary<string, List<string>?> _errorsByPropertyName = [];
    public bool HasErrors => _errorsByPropertyName.Any();
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public IEnumerable? GetErrors(string? propertyName)
    {
        return propertyName != null && _errorsByPropertyName.ContainsKey(propertyName)
            ? _errorsByPropertyName[propertyName]
            : null;
    }

    protected virtual void OnErrorsChanged(DataErrorsChangedEventArgs e)
    {
        ErrorsChanged?.Invoke(this, e);
    }

    protected void AddError(string error, [CallerMemberName] string? propertyName = null)
    {
        if (propertyName == null) return;
        if (!_errorsByPropertyName.ContainsKey(propertyName))
        {
            _errorsByPropertyName[propertyName] = new List<string>();
        }

        if (!_errorsByPropertyName[propertyName]!.Contains(error))
        {
            _errorsByPropertyName[propertyName]!.Add(error);
            OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
            RaisePropertyChanged(nameof(HasErrors));
        }
    }

    protected void RemoveError([CallerMemberName] string? propertyName = null)
    {
        if (propertyName == null) return;
        if (_errorsByPropertyName.ContainsKey(propertyName) == false)
            return;

        _errorsByPropertyName.Remove(propertyName);
        OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
        RaisePropertyChanged(nameof(HasErrors));
    }
}
