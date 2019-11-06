using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0.15,0,0,0,0]")]
	public partial class TrainNetworkObject : NetworkObject
	{
		public const int IDENTITY = 7;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private float _Rotation;
		public event FieldEvent<float> RotationChanged;
		public InterpolateFloat RotationInterpolation = new InterpolateFloat() { LerpT = 0.15f, Enabled = true };
		public float Rotation
		{
			get { return _Rotation; }
			set
			{
				// Don't do anything if the value is the same
				if (_Rotation == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_Rotation = value;
				hasDirtyFields = true;
			}
		}

		public void SetRotationDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_Rotation(ulong timestep)
		{
			if (RotationChanged != null) RotationChanged(_Rotation, timestep);
			if (fieldAltered != null) fieldAltered("Rotation", _Rotation, timestep);
		}
		[ForgeGeneratedField]
		private int _From;
		public event FieldEvent<int> FromChanged;
		public Interpolated<int> FromInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int From
		{
			get { return _From; }
			set
			{
				// Don't do anything if the value is the same
				if (_From == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_From = value;
				hasDirtyFields = true;
			}
		}

		public void SetFromDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_From(ulong timestep)
		{
			if (FromChanged != null) FromChanged(_From, timestep);
			if (fieldAltered != null) fieldAltered("From", _From, timestep);
		}
		[ForgeGeneratedField]
		private int _To;
		public event FieldEvent<int> ToChanged;
		public Interpolated<int> ToInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int To
		{
			get { return _To; }
			set
			{
				// Don't do anything if the value is the same
				if (_To == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_To = value;
				hasDirtyFields = true;
			}
		}

		public void SetToDirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_To(ulong timestep)
		{
			if (ToChanged != null) ToChanged(_To, timestep);
			if (fieldAltered != null) fieldAltered("To", _To, timestep);
		}
		[ForgeGeneratedField]
		private int _Responder;
		public event FieldEvent<int> ResponderChanged;
		public Interpolated<int> ResponderInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int Responder
		{
			get { return _Responder; }
			set
			{
				// Don't do anything if the value is the same
				if (_Responder == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x8;
				_Responder = value;
				hasDirtyFields = true;
			}
		}

		public void SetResponderDirty()
		{
			_dirtyFields[0] |= 0x8;
			hasDirtyFields = true;
		}

		private void RunChange_Responder(ulong timestep)
		{
			if (ResponderChanged != null) ResponderChanged(_Responder, timestep);
			if (fieldAltered != null) fieldAltered("Responder", _Responder, timestep);
		}
		[ForgeGeneratedField]
		private bool _Moving;
		public event FieldEvent<bool> MovingChanged;
		public Interpolated<bool> MovingInterpolation = new Interpolated<bool>() { LerpT = 0f, Enabled = false };
		public bool Moving
		{
			get { return _Moving; }
			set
			{
				// Don't do anything if the value is the same
				if (_Moving == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x10;
				_Moving = value;
				hasDirtyFields = true;
			}
		}

		public void SetMovingDirty()
		{
			_dirtyFields[0] |= 0x10;
			hasDirtyFields = true;
		}

		private void RunChange_Moving(ulong timestep)
		{
			if (MovingChanged != null) MovingChanged(_Moving, timestep);
			if (fieldAltered != null) fieldAltered("Moving", _Moving, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			RotationInterpolation.current = RotationInterpolation.target;
			FromInterpolation.current = FromInterpolation.target;
			ToInterpolation.current = ToInterpolation.target;
			ResponderInterpolation.current = ResponderInterpolation.target;
			MovingInterpolation.current = MovingInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _Rotation);
			UnityObjectMapper.Instance.MapBytes(data, _From);
			UnityObjectMapper.Instance.MapBytes(data, _To);
			UnityObjectMapper.Instance.MapBytes(data, _Responder);
			UnityObjectMapper.Instance.MapBytes(data, _Moving);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_Rotation = UnityObjectMapper.Instance.Map<float>(payload);
			RotationInterpolation.current = _Rotation;
			RotationInterpolation.target = _Rotation;
			RunChange_Rotation(timestep);
			_From = UnityObjectMapper.Instance.Map<int>(payload);
			FromInterpolation.current = _From;
			FromInterpolation.target = _From;
			RunChange_From(timestep);
			_To = UnityObjectMapper.Instance.Map<int>(payload);
			ToInterpolation.current = _To;
			ToInterpolation.target = _To;
			RunChange_To(timestep);
			_Responder = UnityObjectMapper.Instance.Map<int>(payload);
			ResponderInterpolation.current = _Responder;
			ResponderInterpolation.target = _Responder;
			RunChange_Responder(timestep);
			_Moving = UnityObjectMapper.Instance.Map<bool>(payload);
			MovingInterpolation.current = _Moving;
			MovingInterpolation.target = _Moving;
			RunChange_Moving(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _Rotation);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _From);
			if ((0x4 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _To);
			if ((0x8 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _Responder);
			if ((0x10 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _Moving);

			// Reset all the dirty fields
			for (int i = 0; i < _dirtyFields.Length; i++)
				_dirtyFields[i] = 0;

			return dirtyFieldsData;
		}

		protected override void ReadDirtyFields(BMSByte data, ulong timestep)
		{
			if (readDirtyFlags == null)
				Initialize();

			Buffer.BlockCopy(data.byteArr, data.StartIndex(), readDirtyFlags, 0, readDirtyFlags.Length);
			data.MoveStartIndex(readDirtyFlags.Length);

			if ((0x1 & readDirtyFlags[0]) != 0)
			{
				if (RotationInterpolation.Enabled)
				{
					RotationInterpolation.target = UnityObjectMapper.Instance.Map<float>(data);
					RotationInterpolation.Timestep = timestep;
				}
				else
				{
					_Rotation = UnityObjectMapper.Instance.Map<float>(data);
					RunChange_Rotation(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (FromInterpolation.Enabled)
				{
					FromInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					FromInterpolation.Timestep = timestep;
				}
				else
				{
					_From = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_From(timestep);
				}
			}
			if ((0x4 & readDirtyFlags[0]) != 0)
			{
				if (ToInterpolation.Enabled)
				{
					ToInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					ToInterpolation.Timestep = timestep;
				}
				else
				{
					_To = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_To(timestep);
				}
			}
			if ((0x8 & readDirtyFlags[0]) != 0)
			{
				if (ResponderInterpolation.Enabled)
				{
					ResponderInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					ResponderInterpolation.Timestep = timestep;
				}
				else
				{
					_Responder = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_Responder(timestep);
				}
			}
			if ((0x10 & readDirtyFlags[0]) != 0)
			{
				if (MovingInterpolation.Enabled)
				{
					MovingInterpolation.target = UnityObjectMapper.Instance.Map<bool>(data);
					MovingInterpolation.Timestep = timestep;
				}
				else
				{
					_Moving = UnityObjectMapper.Instance.Map<bool>(data);
					RunChange_Moving(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (RotationInterpolation.Enabled && !RotationInterpolation.current.UnityNear(RotationInterpolation.target, 0.0015f))
			{
				_Rotation = (float)RotationInterpolation.Interpolate();
				//RunChange_Rotation(RotationInterpolation.Timestep);
			}
			if (FromInterpolation.Enabled && !FromInterpolation.current.UnityNear(FromInterpolation.target, 0.0015f))
			{
				_From = (int)FromInterpolation.Interpolate();
				//RunChange_From(FromInterpolation.Timestep);
			}
			if (ToInterpolation.Enabled && !ToInterpolation.current.UnityNear(ToInterpolation.target, 0.0015f))
			{
				_To = (int)ToInterpolation.Interpolate();
				//RunChange_To(ToInterpolation.Timestep);
			}
			if (ResponderInterpolation.Enabled && !ResponderInterpolation.current.UnityNear(ResponderInterpolation.target, 0.0015f))
			{
				_Responder = (int)ResponderInterpolation.Interpolate();
				//RunChange_Responder(ResponderInterpolation.Timestep);
			}
			if (MovingInterpolation.Enabled && !MovingInterpolation.current.UnityNear(MovingInterpolation.target, 0.0015f))
			{
				_Moving = (bool)MovingInterpolation.Interpolate();
				//RunChange_Moving(MovingInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public TrainNetworkObject() : base() { Initialize(); }
		public TrainNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public TrainNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
