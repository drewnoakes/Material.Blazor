﻿using Material.Blazor.Internal;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Material.Blazor
{
    /// <summary>
    /// A Material Theme single-thumb slider.
    /// </summary>
    public partial class MBSlider : InputComponent<decimal>
    {
        /// <summary>
        /// The type of slider - defaults to <see cref="MBSliderType.Continuous"/>.
        /// </summary>
        [Parameter] public MBSliderType SliderType { get; set; } = MBSliderType.Continuous;


        /// <summary>
        /// The minimum slider value.
        /// </summary>
        [Parameter] public decimal ValueMin { get; set; } = 0;


        /// <summary>
        /// The maximum slider value.
        /// </summary>
        [Parameter] public decimal ValueMax { get; set; } = 100;


        /// <summary>
        /// The number of steps for a discrete slider. Note that ten steps will yield eleven thumb positions/tickmarks.
        /// </summary>
        [Parameter] public int NumSteps { get; set; } = 10;


        /// <summary>
        /// Specifies how slider events are emitted, see <see cref="MBInputEventType"/>.
        /// </summary>
        [Parameter] public MBInputEventType EventType { get; set; } = MBInputEventType.OnChange;


        /// <summary>
        /// For continuous input sets the debounce/throttle delay.
        /// </summary>
        [Parameter] public uint ContinuousInputDelay { get; set; } = 300;


        /// <summary>
        /// Number of decimal places for rounding and displaying values.
        /// </summary>
        [Parameter] public uint DecimalPlaces { get; set; } = 0;


        /// <summary>
        /// Value for the "aria-label" tag.
        /// </summary>
        [Parameter] public string AriaLabel { get; set; } = "Slider";


        private decimal LocalValue { get; set; }
        private decimal LocalValueMin { get; set; }
        private decimal LocalValueMax { get; set; }
        private ElementReference ElementReference { get; set; }
        private string Format { get; set; }
        private MarkupString InputMarkup { get; set; }
        private DotNetObjectReference<MBSlider> ObjectReference { get; set; }
        private decimal RangePercentDecimal { get; set; }
        private int TabIndex { get; set; }
        private decimal ThumbEndPercent => 100 * RangePercentDecimal;
        private decimal ValueStepIncrement { get; set; }
        // Delete once https://github.com/material-components/material-components-web/issues/7404 is fixed
        private decimal Mult { get; set; }


        // Would like to use <inheritdoc/> however DocFX cannot resolve to references outside Material.Blazor
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (ValueMax <= ValueMin)
            {
                throw new ArgumentException($"{GetType()} must have ValueMax ({ValueMax}) greater than ValueMin ({ValueMin})");
            }

            if (Value < ValueMin || Value > ValueMax)
            {
                throw new ArgumentException($"{GetType()} must have Value ({Value}) between ValueMin ({ValueMin}) and ValueMax ({ValueMax})");
            }

            // Reinstate these 4 lines once https://github.com/material-components/material-components-web/issues/7404 is fixed
            //Value = Math.Round(Value, (int)DecimalPlaces);
            //ValueMin = Math.Round(ValueMin, (int)DecimalPlaces);
            //ValueMax = Math.Round(ValueMax, (int)DecimalPlaces);
            //Format = $"F{DecimalPlaces}";

            // Delete once https://github.com/material-components/material-components-web/issues/7404 is fixed
            if (SliderType == MBSliderType.Continuous)
            {
                Mult = Convert.ToDecimal(Math.Pow(10, DecimalPlaces));
            }
            else
            {
                Mult = NumSteps / (ValueMax - ValueMin);
            }

            LocalValue = Math.Round(Value * Mult, (int)DecimalPlaces);
            LocalValueMin = Math.Round(ValueMin * Mult, (int)DecimalPlaces);
            LocalValueMax = Math.Round(ValueMax * Mult, (int)DecimalPlaces);
            Format = $"F0";
            ValueStepIncrement = 1;
            // End delete

            TabIndex = AppliedDisabled ? -1 : 0;
            RangePercentDecimal = (Value - ValueMin) / (ValueMax - ValueMin);

            // Reinstate once https://github.com/material-components/material-components-web/issues/7404 is fixed
            //if (SliderType == MBSliderType.Continuous)
            //{
            //    ValueStepIncrement = Convert.ToDecimal(Math.Pow(10, -DecimalPlaces));
            //}
            //else
            //{
            //    ValueStepIncrement = (ValueMax - ValueMin) / NumSteps;
            //}

            ConditionalCssClasses
                .AddIf("mdc-slider--discrete", () => SliderType != MBSliderType.Continuous)
                .AddIf("mdc-slider--tick-marks", () => SliderType == MBSliderType.DiscreteWithTickmarks)
                .AddIf("mdc-slider--disabled", () => AppliedDisabled);

            InputMarkup = new($"<input class=\"mdc-slider__input\" type=\"range\" value=\"{LocalValue.ToString(Format)}\" step=\"{ValueStepIncrement}\" min=\"{LocalValueMin.ToString(Format)}\" max=\"{LocalValueMax.ToString(Format)}\" name=\"volume\" aria-label=\"{AriaLabel}\">");

            SetComponentValue += OnValueSetCallback;
            OnDisabledSet += OnDisabledSetCallback;

            ObjectReference = DotNetObjectReference.Create(this);
        }


        private bool _disposed = false;
        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                ObjectReference?.Dispose();
            }

            _disposed = true;

            base.Dispose(disposing);
        }


        /// <summary>
        /// For Material Theme to notify of slider value changes via JS Interop.
        /// </summary>
        [JSInvokable]
        public void NotifyChanged(decimal value)
        {
            // Reinstate once https://github.com/material-components/material-components-web/issues/7404 is fixed
            //ComponentValue = value;

            // Delete once https://github.com/material-components/material-components-web/issues/7404 is fixed
            ComponentValue = value / Mult;
        }


        /// <summary>
        /// Callback for value the value setter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnValueSetCallback() => InvokeAsync(() => InvokeJsVoidAsync("MaterialBlazor.MBSlider.setValue", ElementReference, Math.Round(Value * Mult, 0)));


        /// <summary>
        /// Callback for value the Disabled value setter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnDisabledSetCallback() => InvokeAsync(() => InvokeJsVoidAsync("MaterialBlazor.MBSlider.setDisabled", ElementReference, AppliedDisabled));


        /// <inheritdoc/>
        private protected override Task InstantiateMcwComponent() => InvokeJsVoidAsync("MaterialBlazor.MBSlider.init", ElementReference, ObjectReference, EventType, ContinuousInputDelay);
    }
}
