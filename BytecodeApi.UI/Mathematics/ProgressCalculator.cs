using System;

namespace BytecodeApi.UI.Mathematics
{
	/// <summary>
	/// Represents a performance counter for progress calculations, typically used in binary transfers (file transferring, downloads, ...)
	/// </summary>
	public class ProgressCalculator
	{
		private double _Value;
		private DateTime LastMeasuredTime;
		private double LastMeasuredValue;
		/// <summary>
		/// Gets or sets the value indicating the total progress. This is typically the amount of transferred bytes in a binary transfer operation.
		/// </summary>
		public double Value
		{
			get => _Value;
			set
			{
				bool measured = false;
				DateTime now = DateTime.Now;
				if (now - LastMeasuredTime > MeasureTimeSpan)
				{
					LastMeasuredTime = now;
					Difference = value - LastMeasuredValue;
					LastMeasuredValue = value;

					measured = true;
				}
				_Value = value;

				ProgressCalculatorEventArgs e = new ProgressCalculatorEventArgs(Value, Progress, ProgressPercentage, Difference, DifferencePerSecond);

				OnValueChanged(e);
				if (measured) OnMeasured(e);
			}
		}
		/// <summary>
		/// Gets or sets the expected maximum value. This is typically the total size in bytes in a binary transfer operation. If <see cref="MaxValue" /> is <see langword="null" />, values calculated based on it will also be <see langword="null" />. This is useful for display of an indeterminate status of progress bars.
		/// </summary>
		public double? MaxValue { get; set; }
		/// <summary>
		/// Gets or sets the interval in which <see cref="Value" /> is measured. The <see cref="Measured" /> event is raised in the approximate interval of this value, however only after <see cref="Value" /> has changed.
		/// </summary>
		public TimeSpan MeasureTimeSpan { get; set; }
		/// <summary>
		/// Gets a <see cref="double" /> value indicating the progress. This value is between 0.0 and 1.0, if <see cref="Value" /> is in bounds of 0.0 and <see cref="MaxValue" />.
		/// </summary>
		public double? Progress => MaxValue == null ? (double?)null : Value == 0 ? 1 : Value / MaxValue.Value;
		/// <summary>
		/// Gets a <see cref="double" /> value indicating the progress in percent. This value is between 0.0 and 100.0, if <see cref="Value" /> is in bounds of 0.0 and <see cref="MaxValue" />.
		/// </summary>
		public double? ProgressPercentage => Progress * 100;
		/// <summary>
		/// Gets the difference of the <see cref="Value" /> property between the last two measurements.
		/// </summary>
		public double Difference { get; private set; }
		/// <summary>
		/// Gets the difference of the <see cref="Value" /> property between the last two measurements, interpolated to one second based on <see cref="MeasureTimeSpan" />.
		/// </summary>
		public double DifferencePerSecond => MeasureTimeSpan > TimeSpan.Zero ? Difference / MeasureTimeSpan.TotalSeconds : double.NaN;
		/// <summary>
		/// Occurs when the <see cref="Value" /> property changed.
		/// </summary>
		public event EventHandler<ProgressCalculatorEventArgs> ValueChanged;
		/// <summary>
		/// Occurs when the <see cref="Value" /> property changed and the interval specified in <see cref="MeasureTimeSpan" /> has been reached.
		/// </summary>
		public event EventHandler<ProgressCalculatorEventArgs> Measured;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProgressCalculator" /> class with no maximum value and a measurement interval of 1 second.
		/// </summary>
		public ProgressCalculator()
		{
			MeasureTimeSpan = TimeSpan.FromSeconds(1);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="ProgressCalculator" /> class with the specified maximum value and a measurement interval of 1 second.
		/// </summary>
		/// <param name="maxValue">A <see cref="double" /> value indicating the maximum expected value for <see cref="Value" />.</param>
		public ProgressCalculator(double? maxValue) : this()
		{
			MaxValue = maxValue;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="ProgressCalculator" /> class with the specified maximum value and the specified measurement interval.
		/// </summary>
		/// <param name="maxValue">A <see cref="double" /> value indicating the maximum expected value for <see cref="Value" />.</param>
		/// <param name="measureTimeSpan">A <see cref="TimeSpan" /> value indicating the interval in which to measure the progress.</param>
		public ProgressCalculator(double? maxValue, TimeSpan measureTimeSpan) : this(maxValue)
		{
			MeasureTimeSpan = measureTimeSpan;
		}

		/// <summary>
		/// Raises the <see cref="ValueChanged" /> event.
		/// </summary>
		/// <param name="e">The event data for the <see cref="ValueChanged" /> event.</param>
		protected void OnValueChanged(ProgressCalculatorEventArgs e)
		{
			ValueChanged?.Invoke(this, e);
		}
		/// <summary>
		/// Raises the <see cref="Measured" /> event.
		/// </summary>
		/// <param name="e">The event data for the <see cref="Measured" /> event.</param>
		protected void OnMeasured(ProgressCalculatorEventArgs e)
		{
			Measured?.Invoke(this, e);
		}
	}
}