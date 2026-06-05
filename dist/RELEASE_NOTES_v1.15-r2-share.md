## KeePass2Android 1.15-r2 (Net) — Share fork

### New
- **Share** menu item on each entry field popup (Share / Поделиться)
- Sends field value as plain text via Android system share sheet (`ACTION_SEND`)

### Build
- Self-contained APK (`EmbedAssembliesIntoApk`, fast deployment disabled)
- Safe to install with `adb install` without Visual Studio

### Install
```bash
adb install -r keepass2android-1.15-r2-share-net.apk
```

Package: `keepass2android.keepass2android` (Net flavor)
