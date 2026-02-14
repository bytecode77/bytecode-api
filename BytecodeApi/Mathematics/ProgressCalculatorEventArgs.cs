namespace BytecodeApi.Mathematics;

/// <summary>
/// Provides data for <see cref="ProgressCalculator" /> events.
/// </summary>
public sealed class ProgressCalculatorEventArgs : EventArgs
{
	/// <summary>
	/// Gets the value indicating the total progress. This is typically the amount of transferred bytes in a binary transfer operation.
	/// </summary>
	public double Value { get; }
	/// <summary>
	/// Gets a <see cref="double" /> value indicating the progress. This value is between 0.0 and 1.0, if <see cref="Value" /> is in bounds of 0.0 and <see cref="ProgressCalculator.MaxValue" />.
	/// </summary>
	public double? Progress { get; }
	/// <summary>
	/// Gets a <see cref="double" /> value indicating the progress in percent. This value is between 0.0 and 100.0, if <see cref="Value" /> is in bounds of 0.0 and <see cref="ProgressCalculator.MaxValue" />.
	/// </summary>
	public double? ProgressPercentage { get; }
	/// <summary>
	/// Gets the difference of the <see cref="Value" /> property between the last two measurements.
	/// </summary>
	public double Difference { get; }
	/// <summary>
	/// Gets the difference of the <see cref="Value" /> property between the last two measurements, interpolated to one second based on <see cref="ProgressCalculator.MeasureTimeSpan" />.
	/// </summary>
	public double DifferencePerSecond { get; }

	/// <summary>
	/// Initializes a new instance of the <see cref="ProgressCalculatorEventArgs" /> class using the specified properties equivalent to the properties of <see cref="ProgressCalculator" />.
	/// </summary>
	/// <param name="value">The value representing the <see cref="ProgressCalculator.Value" /> property.</param>
	/// <param name="progress">The value representing the <see cref="ProgressCalculator.Progress" /> property.</param>
	/// <param name="progressPercentage">The value representing the <see cref="ProgressCalculator.ProgressPercentage" /> property.</param>
	/// <param name="difference">The value representing the <see cref="ProgressCalculator.Difference" /> property.</param>
	/// <param name="differencePerSecond">The value representing the <see cref="ProgressCalculator.DifferencePerSecond" /> property.</param>
	public ProgressCalculatorEventArgs(double value, double? progress, double? progressPercentage, double difference, double differencePerSecond)
	{
		Value = value;
		Progress = progress;
		ProgressPercentage = progressPercentage;
		Difference = difference;
		DifferencePerSecond = differencePerSecond;
	}
}