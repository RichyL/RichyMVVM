using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;

namespace RichyMVVM.Framework.In_Development
{
    /// <summary>
    /// A BindableProperty (formerly an AttachedProperty in WPF) that allows a new property to be added to a type of object. The aim would be to have this as a BindableProperty 
    /// on ContentView which is bound to a ViewModel property of type ObservableObject. This would mean if a ViewModel has child ViewModels (hence the ObservableObject)
    /// then this property would find the relevant View for that ViewModel and create an instance of it and bind it to ObservableObject.
    /// 
    /// A use case would be for a custom control which had its own ViewModel. 
    /// </summary>
    /// <remarks>This class is not fully implemented</remarks>
    public static class Linker
    {
        #region BindView
        public static ObservableObject GetBindView(BindableObject target)
        {
            return (ObservableObject)target.GetValue(BindViewProperty);
        }

        public static void SetBindView(BindableObject target, ObservableObject observableObject)
        {
            target.SetValue(BindViewProperty, observableObject);
        }

        // Using a DependencyProperty as the backing store for observableObject.  This enables animation, styling, binding, etc...
        public static readonly BindableProperty BindViewProperty =
            BindableProperty.CreateAttached(propertyName: "BindView",
                returnType: typeof(ObservableObject),
                declaringType: typeof(Linker),
                defaultValue: null,
                defaultBindingMode: BindingMode.OneWay,
                validateValue: null,
                propertyChanged: ObservableObjectPropertyChanged);

        private static void ObservableObjectPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            Debug.WriteLine("From linker");
            Debug.WriteLine($"DependencyObject {bindable}");
            Debug.WriteLine($"oldValue {oldValue}");
            Debug.WriteLine($"newValue {newValue}");

            //get the name of the viewmodel type
            string viewModelTypeName = newValue.GetType().AssemblyQualifiedName;
            Debug.WriteLine($"viewModelTypeName {viewModelTypeName}");


            //remove the word Model and this will be the name of the View type
            string viewTypeName = "BasicLaunch.FirstView, BasicLaunch, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";




            //create the viewtype and bind to control

            Type viewType = Type.GetType(viewTypeName);
            View newView = (View)Activator.CreateInstance(viewType);
            newView.BindingContext = GetBindView(bindable);

            ContentView bindableView = (ContentView)bindable;
            bindableView.Content = newView;


            Debug.WriteLine("******************************* Bindview End");
        }
        #endregion

        #region ColorMeRed

        public static readonly BindableProperty ColorMeRedProperty =
        BindableProperty.CreateAttached("ColorMeRed", typeof(Brush), typeof(ContentView), Brush.Yellow);

        public static Brush GetColorMeRed(BindableObject target)
        {
            return (Brush)target.GetValue(ColorMeRedProperty);
        }

        public static void SetColorMeRed(BindableObject bindableObject, Brush value)
        {
            bindableObject.SetValue(ColorMeRedProperty, value);
        }

        #endregion


    }
}
