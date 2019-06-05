# Using git

Git continue to track any files that are already being tracked.
To stop tracking a file you need to remove it from the index.

```CLI
git rm --cached <file>

git rm --cached appsettings.json
```

## Add kdiff3 as difftool and mergetool

```
git config --global --add merge.tool kdiff3
git config --global --add mergetool.kdiff3.path "C:/Apps/KDiff3/kdiff3.exe"
git config --global --add mergetool.kdiff3.trustExitCode false

git config --global --add diff.guitool kdiff3
git config --global --add difftool.kdiff3.path "C:/Apps/KDiff3/kdiff3.exe"
git config --global --add difftool.kdiff3.trustExitCode false
```

## Config a add-on tool to git-gui

```
[guitool "difftool"]
	cmd = git difftool -y $FILENAME
	noconsole = yes
	needsfile = yes
```