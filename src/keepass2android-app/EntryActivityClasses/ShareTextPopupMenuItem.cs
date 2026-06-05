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

namespace keepass2android
{
  /// <summary>
  /// Popup menu item in EntryActivity to share a single field value as plain text via the system share sheet.
  /// </summary>
  class ShareTextPopupMenuItem : IPopupMenuItem
  {
    private readonly Activity _activity;
    private readonly IStringView _stringView;

    public ShareTextPopupMenuItem(Activity activity, IStringView stringView)
    {
      _activity = activity;
      _stringView = stringView;
    }

    public Drawable Icon =>
        _activity.Resources.GetDrawable(Resource.Drawable.baseline_share_24);

    public string Text =>
        _activity.Resources.GetString(Resource.String.share_text);

    public void HandleClick()
    {
      string text = _stringView.Text;
      if (string.IsNullOrEmpty(text))
        return;

      Intent sendIntent = new Intent(Intent.ActionSend);
      sendIntent.SetType("text/plain");
      sendIntent.PutExtra(Intent.ExtraText, text);
      _activity.StartActivity(Intent.CreateChooser(sendIntent, Text));
    }
  }
}
