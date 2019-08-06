using System;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.Contrib
{
    /// <summary>
    /// Renders a ...
    /// </summary>
    public class GoogleMap : HtmlGenericControl
    {
        public string Address
        {
            get => (string)GetValue(AddressProperty);
            set => SetValue(AddressProperty, value);
        }

        public static readonly DotvvmProperty AddressProperty
            = DotvvmProperty.Register<string, GoogleMap>(c => c.Address, null);


        public float Longitude
        {
            get { return (float)GetValue(LongitudeProperty); }
            set { SetValue(LongitudeProperty, value); }
        }
        public static readonly DotvvmProperty LongitudeProperty
            = DotvvmProperty.Register<float, GoogleMap>(c => c.Longitude, 0);

        public float Latitude
        {
            get { return (float)GetValue(LatitudeProperty); }
            set { SetValue(LatitudeProperty, value); }
        }
        public static readonly DotvvmProperty LatitudeProperty
            = DotvvmProperty.Register<float, GoogleMap>(c => c.Latitude, 0);

        public int MapZoom
        {
            get { return (int)GetValue(MapZoomProperty); }
            set { SetValue(MapZoomProperty, value); }
        }
        public static readonly DotvvmProperty MapZoomProperty
            = DotvvmProperty.Register<int, GoogleMap>(c => c.MapZoom, 15);



        public GoogleMap() : base("div")
        {
        }

        protected override void OnInit(IDotvvmRequestContext context)
        {
            if (IsPropertySet(LongitudeProperty) && !IsPropertySet(LatitudeProperty) || !IsPropertySet(LongitudeProperty) && IsPropertySet(LatitudeProperty))
                throw new DotvvmControlException($"Both {nameof(Longitude)} and {nameof(Latitude)} must be set.");

            if (IsPropertySet(AddressProperty) && (IsPropertySet(LongitudeProperty) || IsPropertySet(LatitudeProperty)))
                throw new DotvvmControlException(this,$"Address and {nameof(Longitude)}/{nameof(Latitude)} cannot be set at the same time.");

            if (!IsPropertySet(AddressProperty) && !(IsPropertySet(LongitudeProperty) && IsPropertySet(LatitudeProperty)))
                throw new DotvvmControlException(this,$"Address or {nameof(Longitude)} + {nameof(Latitude)} must be set");
            

            context.ResourceManager.AddRequiredResource("dotvvm.contrib.GoogleMap");
            base.OnInit(context);
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.AddAttribute("class", "dotvvm-google-map");

            writer.AddKnockoutDataBind("dotvvm-contrib-GoogleMap-Address", this, AddressProperty, () =>
            {
                if (IsPropertySet(AddressProperty))
                    writer.AddKnockoutDataBind("dotvvm-contrib-GoogleMap-Address", $"'{Address}'");
            });

            writer.AddKnockoutDataBind("dotvvm-contrib-GoogleMap-Latitude", this, LatitudeProperty, () =>
            {
                if (IsPropertySet(LatitudeProperty))
                    writer.AddKnockoutDataBind("dotvvm-contrib-GoogleMap-Latitude", Latitude.ToString());
            });

            writer.AddKnockoutDataBind("dotvvm-contrib-GoogleMap-Longitude", this, LongitudeProperty, () =>
            {
                if (IsPropertySet(LongitudeProperty))
                    writer.AddKnockoutDataBind("dotvvm-contrib-GoogleMap-Longitude", Longitude.ToString());
            });
            writer.AddKnockoutDataBind("dotvvm-contrib-GoogleMap-MapZoom", this, MapZoomProperty, () =>
            {
                writer.AddKnockoutDataBind("dotvvm-contrib-GoogleMap-MapZoom", MapZoom.ToString());

            });

            base.AddAttributesToRender(writer, context);
        }
    }
}
