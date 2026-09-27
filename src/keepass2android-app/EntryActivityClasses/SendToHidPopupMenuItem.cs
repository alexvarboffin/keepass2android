// This file is part of Keepass2Android, Copyright 2025 Philipp Crocoll.
//
//   Keepass2Android is free software: you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.
//
//   Keepass2Android is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU General Public License for more details.
//
//   You should have received a copy of the GNU General Public License
//   along with Keepass2Android.  If not, see <http://www.gnu.org/licenses/>.

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Widget;

namespace keepass2android
{
  /// <summary>
  /// Popup menu item in EntryActivity that types a field value through the Bluetooth HID keyboard app.
  /// </summary>
  class SendToHidPopupMenuItem : IPopupMenuItem
  {
    private const string HidPackage = "com.walhalla.bluetoothhiddevice";
    private const string HidServiceClass = "com.walhalla.bluetoothhiddevice.HidTextService";
    private const string ActionSendText = "com.walhalla.bluetoothhiddevice.action.SEND_TEXT";
    private const string ExtraText = "text";

    private readonly Activity _activity;
    private readonly IStringView _stringView;

    public SendToHidPopupMenuItem(Activity activity, IStringView stringView)
    {
      _activity = activity;
      _stringView = stringView;
    }

    public Drawable Icon =>
        _activity.Resources.GetDrawable(Resource.Drawable.baseline_keyboard_24);

    public string Text =>
        _activity.Resources.GetString(Resource.String.send_to_hid);

    public void HandleClick()
    {
      string text = _stringView.Text;
      if (string.IsNullOrEmpty(text))
        return;

      try
      {
        Intent intent = new Intent(ActionSendText);
        intent.SetComponent(new ComponentName(HidPackage, HidServiceClass));
        intent.PutExtra(ExtraText, text);
        _activity.StartService(intent);
      }
      catch (Exception)
      {
        Toast.MakeText(_activity, Resource.String.send_to_hid_unavailable, ToastLength.Short).Show();
      }
    }
  }
}
