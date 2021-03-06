﻿using System;
using System.IO;

namespace tRSS.Utilities
{
	/// <summary>
	/// Description of Path.
	/// </summary>
	public static class Paths
	{
		// HACK Not sure if paths work
		
		public static string MakeRelativePath(string fromPath, string toPath)
		{
			if (String.IsNullOrEmpty(fromPath)) throw new ArgumentNullException("fromPath");
			if (String.IsNullOrEmpty(toPath))   throw new ArgumentNullException("toPath");
			
			Uri fromUri = new Uri(FixEnding(fromPath));
			Uri toUri = new Uri(FixEnding(toPath));
			
			
			if (fromUri.Scheme != toUri.Scheme) { return toPath; } // path can't be made relative.
			if (fromUri.Equals(toUri)) { return Path.DirectorySeparatorChar.ToString(); }
			
			Uri relativeUri = fromUri.MakeRelativeUri(toUri);
			return Uri.UnescapeDataString(relativeUri.ToString());
		}
		
		public static string FixEnding(string path)
		{
			if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
			{
				if (File.GetAttributes(path) == FileAttributes.Directory)
				{
					return path + Path.DirectorySeparatorChar;
				}
			}
			return path;
		}
		
		public static string MakeRelativePath(string path)
		{
			return MakeRelativePath(AppDomain.CurrentDomain.BaseDirectory, path);
		}
	}
}
