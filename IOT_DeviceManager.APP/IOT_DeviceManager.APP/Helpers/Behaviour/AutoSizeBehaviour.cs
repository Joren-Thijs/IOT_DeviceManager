﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IOT_DeviceManager.APP.Helpers.Behaviour
{
    public class AutoSizeBehavior : Behavior<ListView>
    {
        ListView _ListView;
        ITemplatedItemsView<Cell> Cells => _ListView;

        protected override void OnAttachedTo(ListView bindable)
        {
            bindable.ItemAppearing += AppearanceChanged;
            bindable.ItemDisappearing += AppearanceChanged;
            _ListView = bindable;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            bindable.ItemAppearing -= AppearanceChanged;
            bindable.ItemDisappearing -= AppearanceChanged;
            _ListView = null;
        }

        void AppearanceChanged(object sender, ItemVisibilityEventArgs e) =>
            UpdateHeight(e.Item);

        void UpdateHeight(object item)
        {
            if (_ListView.HasUnevenRows)
            {
                double height;
                if ((height = _ListView.HeightRequest) ==
                    (double)VisualElement.HeightRequestProperty.DefaultValue)
                    height = 0;

                height += MeasureRowHeight(item);
                SetHeight(height);
            }
            else if (_ListView.RowHeight == (int)ListView.RowHeightProperty.DefaultValue)
            {
                var height = MeasureRowHeight(item);
                _ListView.RowHeight = height;
                SetHeight(height);
            }
        }

        int MeasureRowHeight(object item)
        {
            var template = _ListView.ItemTemplate;
            var cell = (Cell)template.CreateContent();
            cell.BindingContext = item;
            var height = cell.RenderHeight;
            var mod = height % 1;
            if (mod > 0)
                height = height - mod + 1;
            return (int)height;
        }

        void SetHeight(double height)
        {
            //TODO if header or footer is string etc.
            if (_ListView.Header is VisualElement header)
                height += header.Height;
            if (_ListView.Footer is VisualElement footer)
                height += footer.Height;
            _ListView.HeightRequest = height;
        }
    }
}
