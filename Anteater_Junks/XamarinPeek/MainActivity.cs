using Android.App;
using Android.Widget;
using Android.OS;

namespace XamarinPeek
{
    [Activity(Label = "XamarinPeek", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private Button button1;
        private TextView textView1;
        private bool toggle;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            textView1 = FindViewById<TextView>(Resource.Id.textView1);
            toggle = false;
            button1 = FindViewById<Button>(Resource.Id.button1);
            button1.Click += Button1_Click;
        }

        private void Button1_Click(object sender, System.EventArgs e)
        {
            if (!toggle)
                textView1.Text = "Hello Xamarin!";
            else
                textView1.Text = "Hello World!";
            toggle = !toggle;
        }
    }
}

