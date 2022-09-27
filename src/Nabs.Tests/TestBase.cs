using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Nabs.Tests;

public abstract class TestBase : IDisposable
{
	private readonly TextWriter _originalOut;
	private readonly string _testClassName;

	protected TestBase(
		ITestOutputHelper output)
	{
		_testClassName = this.GetType().Name;

		Output = output;
		var message = $"[{_testClassName}] is constructing...";
		OutputLine(message);
		OutputLine(new string('=', message.Length));

		_originalOut = Console.Out;
		TextWriter = new StringWriter();
		Console.SetOut(TextWriter);
	}

	protected ITestOutputHelper Output { get; }
	protected TextWriter TextWriter { get; }

	public void OutputLine(string message)
	{
		Output.WriteLine(message);
	}

	public virtual void OutputScenario(string scenario = "default", [CallerMemberName] string caller = null)
	{
		OutputLine($"[{caller}] - Scenario: {scenario}");
	}

	public virtual void OutputStep(string stepName)
	{
		OutputLine($"Step: {stepName}");
	}

	public void Dispose()
	{
		var message = $"[{_testClassName}] is disposed!";
		OutputLine(new string('=', message.Length));
		OutputLine(message);
		Console.SetOut(_originalOut);
	}

	
}