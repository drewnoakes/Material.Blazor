﻿using Material.Blazor.Internal;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Material.Blazor
{
    /// <summary>
    /// A date picker styled to match the Material Theme date picker specification, using
    /// a modfied Material Theme select input as also applied in <see cref="MBSelect{TItem}"/>.
    /// </summary>
    public partial class MBDatePicker : InputComponent<DateTime>
    {
        #region members
        internal static readonly DateTime MinAllowableDate = DateTime.MinValue;
        internal static readonly DateTime MaxAllowableDate = DateTime.MaxValue;

        private string AdditionalStyle { get; set; } = "";
        private MBDensity AppliedDensity => CascadingDefaults.AppliedSelectDensity(Density);
        private string AppliedDateFormat => CascadingDefaults.AppliedDateFormat(DateFormat);
        private MBSelectInputStyle AppliedInputStyle => CascadingDefaults.AppliedStyle(SelectInputStyle);
        private ElementReference ElementReference { get; set; }
        private ElementReference MenuSurfaceElementReference { get; set; }
        private DotNetObjectReference<MBDatePicker> ObjectReference { get; set; }
        private string MenuClass => MBMenu.GetMenuSurfacePositioningClass(MenuSurfacePositioning == MBMenuSurfacePositioning.Fixed ? MBMenuSurfacePositioning.Fixed : MBMenuSurfacePositioning.Regular) + ((Panel?.ShowYearPad ?? true) ? " mb-dp-menu__day-menu" : " mb-dp-menu__year-menu");
        private InternalDatePickerPanel Panel { get; set; }
        private bool ShowLabel => !string.IsNullOrWhiteSpace(Label);

        private readonly string invisibleText = "color: rgba(0, 0, 0, 0.0); ";
        private readonly string labelId = Utilities.GenerateUniqueElementName();
        private readonly string listboxId = Utilities.GenerateUniqueElementName();
        private readonly string selectedTextId = Utilities.GenerateUniqueElementName();

        #endregion

        #region parameters


        /// <summary>
        /// Specification for date format
        /// </summary>
        [Parameter] public string DateFormat { get; set; }


#nullable enable annotations
        internal static readonly Func<DateTime, bool> DateIsSelectableNotUsed = (_) => true;
        /// <summary>
        /// Control whether a date is selectable by evaluating the method.
        /// </summary>
        [Parameter] public Func<DateTime, bool>? DateIsSelectable { get; set; } = DateIsSelectableNotUsed;
#nullable restore annotations


        /// <summary>
        /// Date selection criteria
        /// </summary>
        [Parameter] public MBDateSelectionCriteria? DateSelectionCriteria { get; set; }


        /// <summary>
        /// The select's density.
        /// </summary>
        [Parameter] public MBDensity? Density { get; set; }


        /// <summary>
        /// The label.
        /// </summary>
        [Parameter] public string Label { get; set; }


        /// <summary>
        /// Maximum date set by the consumer
        /// </summary>
        [Parameter] public DateTime MaxDate { get; set; } = MaxAllowableDate;


        /// <summary>
        /// Regular, fullwidth or fixed positioning/width. Overrides a value of <see cref="MBMenuSurfacePositioning.FullWidth"/>
        /// with <see cref="MBMenuSurfacePositioning.Regular"/>.
        /// </summary>
        [Parameter] public MBMenuSurfacePositioning MenuSurfacePositioning { get; set; } = MBMenuSurfacePositioning.Regular;


        /// <summary>
        /// Minimum date set by the consumer
        /// </summary>
        [Parameter] public DateTime MinDate { get; set; } = MinAllowableDate;


        /// <summary>
        /// The select style.
        /// <para>Overrides <see cref="MBCascadingDefaults.SelectInputStyle"/></para>
        /// </summary>
        [Parameter] public MBSelectInputStyle? SelectInputStyle { get; set; }


        /// <summary>
        /// Set to indicate that if the value is default(DateTime) then no date is initially shown
        /// and the panel will start with the current year and month
        /// </summary>
        [Parameter] public bool SuppressDefaultDate { get; set; }


        #endregion

        #region DensityInfo

        private MBCascadingDefaults.DensityInfo DensityInfo
        {
            get
            {
                var d = CascadingDefaults.GetDensityCssClass(AppliedDensity);

                d.CssClassName += AppliedInputStyle == MBSelectInputStyle.Filled ? "--filled" : "--outlined";

                return d;
            }
        }

        #endregion

        #region InstantiateMcwComponent

        /// <inheritdoc/>
        private protected override Task InstantiateMcwComponent() => InvokeJsVoidAsync("MaterialBlazor.MBDatePicker.init", ElementReference, MenuSurfaceElementReference, ObjectReference);

        #endregion

        #region OnDisabledSetCallback

        /// <summary>
        /// Callback for value the Disabled value setter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnDisabledSetCallback() => InvokeAsync(() => InvokeJsVoidAsync("MaterialBlazor.MBDatePicker.setDisabled", ElementReference, AppliedDisabled));

        #endregion

        #region NotifyOpened
        /// <summary>
        /// Do not use. This method is used internally for receiving the "dialog closed" event from javascript.
        /// </summary>
        [JSInvokable]
        public async Task NotifyOpened()
        {
            await Panel.NotifyOpened();
        }
        #endregion

        #region OnInitializedAsync

        // Would like to use <inheritdoc/> however DocFX cannot resolve to references outside Material.Blazor
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            ConditionalCssClasses
                .AddIf(DensityInfo.CssClassName, () => DensityInfo.ApplyCssClass)
                .AddIf("mdc-select--filled", () => AppliedInputStyle == MBSelectInputStyle.Filled)
                .AddIf("mdc-select--outlined", () => AppliedInputStyle == MBSelectInputStyle.Outlined)
                .AddIf("mdc-select--no-label", () => string.IsNullOrWhiteSpace(Label))
                .AddIf("mdc-select--disabled", () => AppliedDisabled);

            SetComponentValue += OnValueSetCallback;

            OnDisabledSet += OnDisabledSetCallback;

            // SuppressDefaultDate is only used here and not in SetParametersAsync
            // therefore a change will not be detected (and makes little sense
            // for the component).

            if (SuppressDefaultDate && (Value == default))
            {
                AdditionalStyle = invisibleText;
            }

            ForceShouldRenderToTrue = true;
            
            ObjectReference = DotNetObjectReference.Create(this);
        }

        #endregion

        #region Dispose
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
        #endregion

        #region OnValueSetCallback & NotifyValueChanged

        /// <summary>
        /// Callback for value the value setter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnValueSetCallback()
        {
            Panel.SetParameters(Value);

            if (AdditionalStyle.Length > 0)
            {
                AdditionalStyle = "";
                InvokeAsync(StateHasChanged);
            }

            InvokeAsync(() => InvokeJsVoidAsync("MaterialBlazor.MBDatePicker.listItemClick", Panel.ListItemReference, Utilities.DateToString(Value, AppliedDateFormat)).ConfigureAwait(false));
        }


        /// <summary>
        /// Blanks additional style when the value is changed.
        /// </summary>
        internal void NotifyValueChanged()
        {
            if (AdditionalStyle.Length > 0)
            {
                AdditionalStyle = "";
                InvokeAsync(StateHasChanged);
            }
        }
        #endregion
    }
}
