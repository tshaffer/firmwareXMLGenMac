using System;
using System.Runtime.InteropServices;


namespace FirmwareXMLGen
{
	public static class ProbeWrapper
	{
		public enum Playable_Status
		{
			CP_PLAYABLE             = 0,    // File/component is playable
			CP_PLAYABLE_SEAMLESSLY  = 1,    // File/component is playable seamlessly
			CP_PROBE_STRING         = 100,  // Probe string could not be interpreted ('playable_video' and 'playable_audio' not useful)
			CP_CONTAINER            = 200,  // Container not supported ('playable_video' and 'playable_audio' not useful)
			CP_NO_MEDIA             = 300,  // File/component was not playable
			CP_AUDIO_SAMPLE_RATE    = 400,  // Audio sample rate > 48k
			CP_AUDIO_TYPE           = 401,  // Audio type not supported
			CP_AUDIO_CHANNELS       = 402,  // Audio channels > 2 on unsupported type
			CP_VIDEO_TYPE           = 501,  // Video type not supported
			CP_VIDEO_RESOLUTION     = 502,  // Video resolution not supported
			CP_VIDEO_H265           = 503   // H265 video not supported on this machine
		}

		const int PROBE_FILE_PATH = -1;
		const int PROBE_FILE_PROBE = -2;
		const int PROBE_STREAM_TYPE = -3;
		const int PROBE_BUFFER_SIZE = -4;

		[DllImport("libmedia.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		private static extern int ProbeFile(
			[MarshalAs(UnmanagedType.LPArray)] Char[] file,
			[MarshalAs(UnmanagedType.LPArray)] Char[] buffer,
			int buffer_size);
		// int ProbeFile(const wchar_t *file, wchar_t *buffer, int buffer_size)

		[DllImport("libmedia.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		private static extern int CanPlayFile(
			[MarshalAs(UnmanagedType.LPArray)] Char[] probe_data,
			[MarshalAs(UnmanagedType.LPArray)] Char[] machine,
			[MarshalAs(UnmanagedType.LPArray)] Char[] timeslice_mode,
			ref int playable_video,
			ref int playable_audio);
		// int CanPlayFile(const PROBE_CHAR* probe_data, const PROBE_CHAR* machine, const PROBE_CHAR* timeslice_mode, int* playable_video, int* playable_audio)

		[DllImport("libmedia.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		private static extern int GetDecoderName(
			[MarshalAs(UnmanagedType.LPArray)] Char[] machine,
			int index,
			[MarshalAs(UnmanagedType.LPArray)] Char[] decoder_name_buffer,
			int buffer_size);
		// int GetDecoderName(const TCHAR* machine, int index, TCHAR decoder_name_buffer, int buffer_size)

		[DllImport("libmedia.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		private static extern int GetDecoderTimesliceMaxCount(
			[MarshalAs(UnmanagedType.LPArray)] Char[] machine,
			int index,
			[MarshalAs(UnmanagedType.LPArray)] Char[] timeslice_mode);
		// int GetDecoderTimesliceMaxCount(const TCHAR* machine, int index, const TCHAR* timeslice_mode)

//		public static string GetDecoderName(BrightSignModel model, int index)
//		{
//			Char[] machine = GetMachineName(model);
//
//			string decoderName = String.Empty;
//			Char[] decoder_name_buffer = new char[512];
//
//			int result = GetDecoderName(machine, index, decoder_name_buffer, 512);
//			if (result >= 0)
//			{
//				decoderName = new string(decoder_name_buffer, 0, result);
//			}
//
//			return decoderName;
//		}

		public static string GetProbeData(string filePath)
		{
			string probeData = String.Empty;

			Char[] my_buf = new Char[2048];

//			System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
			Char[] name_buf = filePath.ToCharArray();

			int r = ProbeFile(name_buf, my_buf, 2048);
			if (r >= 0)
			{
				probeData = new string(my_buf, 0, r);
			}
			else if (r == PROBE_FILE_PATH)
			{
				//MessageBox.Show("Can't open given path", "Bad Path",
				//   MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
			else if (r == PROBE_FILE_PROBE)
			{
				//MessageBox.Show("Unknown probe type", "Probe Failure",
				//   MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
			else if (r == PROBE_STREAM_TYPE)
			{
				//MessageBox.Show("Unknown stream type", "Stream Unknown",
				//   MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
			else if (r == PROBE_BUFFER_SIZE)
			{
				//MessageBox.Show("Buffer used is too small", "Buffer Size",
				//   MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
			else
			{
				//MessageBox.Show("Probe returned some unknown error", "Unknown Error",
				//   MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}

			return probeData;
		}


	}
}

