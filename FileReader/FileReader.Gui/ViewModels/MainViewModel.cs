using FileReader.Gui.Helpers;
using FileReader.Lib.Authorization;
using FileReader.Lib.Encryption;
using FileReader.Lib.Enums;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileReader.Gui.ViewModels
{
	public class MainViewModel : NotificationObject
	{
		public MainViewModel()
		{
			InitFileTypesCollection();
			InitUserRolesCollection();
			_fileType = _fileTypes.FirstOrDefault();
			_userRole = _userRoles.FirstOrDefault();
		}

		#region Properties
		private List<string> _fileTypes;
		public List<string> FileTypes
		{
			get => _fileTypes;
			set
			{
				if (_fileTypes == value) return;

				_fileTypes = value;
				RaisePropertyChanged(() => FileTypes);
			}
		}

		private string _fileType;
		public string FileType
		{
			get => _fileType;
			set
			{
				if (_fileType == value) return;

				_fileType = value;
				RaisePropertyChanged(() => FileType);
			}
		}

		private List<string> _userRoles;
		public List<string> UserRoles
		{
			get => _userRoles;
			set
			{
				if (_userRoles == value) return;

				_userRoles = value;
				RaisePropertyChanged(() => UserRoles);
			}
		}

		private string _userRole;
		public string UserRole
		{
			get => _userRole;
			set
			{
				if (_userRole == value) return;

				_userRole = value;
				RaisePropertyChanged(() => UserRoles);
			}
		}

		private string _filePath;
		public string FilePath
		{
			get => _filePath;
			set
			{
				if (_filePath == value) return;

				_filePath = value;
				RaisePropertyChanged(() => FilePath);
			}
		}

		private string _fileContent;
		public string FileContent
		{
			get => _fileContent;
			set
			{
				if (_fileContent == value) return;

				_fileContent = value;
				RaisePropertyChanged(() => FileContent);
			}
		}

		private bool _isSecured;
		public bool IsSecured
		{
			get => _isSecured;
			set
			{
				if (_isSecured == value) return;

				_isSecured = value;
				RaisePropertyChanged(() => IsSecured);
			}
		}

		private bool _isEncrypted;
		public bool IsEncrypted
		{
			get => _isEncrypted;
			set
			{
				if (_isEncrypted == value) return;

				_isEncrypted = value;
				RaisePropertyChanged(() => IsEncrypted);
			}
		}
		#endregion Properties

		#region Commands
		private RelayCommand _browseFileCommand;
		public RelayCommand BrowseFileCommand
		{
			get
			{
				return _browseFileCommand ??= new RelayCommand(param => ExecuteBrowseFileCommand(),
															   param => CanExecuteBrowseFileCommand());
			}
		}

		private bool CanExecuteBrowseFileCommand()
		{
			return true;
		}

		private void ExecuteBrowseFileCommand()
		{
			BrowseFile();
		}

		private RelayCommand _setSecurityCommand;
		public RelayCommand SetSecurityCommand
		{
			get
			{
				return _setSecurityCommand ??= new RelayCommand(param => ExecuteSetSecurityCommand(),
																param => CanExecuteSetSecurityCommand());
			}
		}

		private bool CanExecuteSetSecurityCommand()
		{
			return true;
		}

		private void ExecuteSetSecurityCommand()
		{
			IsSecured = !IsSecured;
		}

		private RelayCommand _setEncryptionCommand;
		public RelayCommand SetEncryptionCommand
		{
			get
			{
				return _setEncryptionCommand ??= new RelayCommand(param => ExecuteSetEncryptionCommand(),
																  param => CanExecuteSetEncryptionCommand());
			}
		}

		private bool CanExecuteSetEncryptionCommand()
		{
			return true;
		}

		private void ExecuteSetEncryptionCommand()
		{
			IsEncrypted = !IsEncrypted;
		}
		#endregion Commands

		#region Helper methods
		private void InitFileTypesCollection()
		{
			_fileTypes = new List<string>();
			var fileTypes = Enum.GetValues(typeof(FileType));
			foreach (var fileType in fileTypes)
			{
				_fileTypes.Add(fileType.ToString());
			}
		}

		private void InitUserRolesCollection()
		{
			_userRoles = new List<string>();
			var userRoles = Enum.GetValues(typeof(UserRole));
			foreach (var fileType in userRoles)
			{
				_userRoles.Add(fileType.ToString());
			}
		}

		private string GetFileFilter()
		{
			var fileType = Utils.ParseEnum<FileType>(FileType);
			var fileTypeName = fileType.ToString();
			var fileTypeExtension = Lib.Utils.FileUtility.GetFileExtensionFromType(fileType);
			return fileTypeExtension == null
				   ? "All files (*.*)|*.*"
				   : $"{fileTypeName} files (*{fileTypeExtension})|*{fileTypeExtension}";
		}

		private void BrowseFile()
		{
			var dialog = new OpenFileDialog
			{
				Filter = GetFileFilter(),
				Multiselect = false
			};

			if (dialog.ShowDialog() == true)
			{
				FilePath = dialog.FileName;
				LoadFileContent();
			}
		}

		private void LoadFileContent()
		{
			var fileReader = CreateFileReader();
			try
			{
				FileContent = fileReader.ReadAsync().Result;
			}
			catch (Exception e)
			{
				FileContent = e.Message;
			}
		}

		private Lib.Readers.FileReader CreateFileReader()
		{
			var fileType = Utils.ParseEnum<FileType>(FileType);
			var builder = new Lib.Readers.FileReaderBuilder(FilePath)
						  .Init(fileType);

			if (IsEncrypted)
			{
				var encryptionStrategy = GetEncryptionStrategy(fileType);
				builder.UseEncryption(encryptionStrategy);
			}

			if (IsSecured)
			{
				var userRole = Utils.ParseEnum<UserRole>(UserRole);
				builder.UseAuthorization(new ReadAuthorization(userRole));
			}


			return builder.Build();
		}

		private static EncryptionStrategy GetEncryptionStrategy(FileType fileType)
		{
			return fileType switch
			{
				Lib.Enums.FileType.Text => new ReverseEncryption(),
				_ => new Base64Encryption()
			};
		}
		#endregion
	}
}
