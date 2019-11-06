using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0,0]")]
	public partial class PlayerNetworkObject : NetworkObject
	{
		public const int IDENTITY = 8;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private int _ID;
		public event FieldEvent<int> IDChanged;
		public Interpolated<int> IDInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int ID
		{
			get { return _ID; }
			set
			{
				// Don't do anything if the value is the same
				if (_ID == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_ID = value;
				hasDirtyFields = true;
			}
		}

		public void SetIDDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_ID(ulong timestep)
		{
			if (IDChanged != null) IDChanged(_ID, timestep);
			if (fieldAltered != null) fieldAltered("ID", _ID, timestep);
		}
		[ForgeGeneratedField]
		private bool _IsSpy;
		public event FieldEvent<bool> IsSpyChanged;
		public Interpolated<bool> IsSpyInterpolation = new Interpolated<bool>() { LerpT = 0f, Enabled = false };
		public bool IsSpy
		{
			get { return _IsSpy; }
			set
			{
				// Don't do anything if the value is the same
				if (_IsSpy == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_IsSpy = value;
				hasDirtyFields = true;
			}
		}

		public void SetIsSpyDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_IsSpy(ulong timestep)
		{
			if (IsSpyChanged != null) IsSpyChanged(_IsSpy, timestep);
			if (fieldAltered != null) fieldAltered("IsSpy", _IsSpy, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			IDInterpolation.current = IDInterpolation.target;
			IsSpyInterpolation.current = IsSpyInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _ID);
			UnityObjectMapper.Instance.MapBytes(data, _IsSpy);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_ID = UnityObjectMapper.Instance.Map<int>(payload);
			IDInterpolation.current = _ID;
			IDInterpolation.target = _ID;
			RunChange_ID(timestep);
			_IsSpy = UnityObjectMapper.Instance.Map<bool>(payload);
			IsSpyInterpolation.current = _IsSpy;
			IsSpyInterpolation.target = _IsSpy;
			RunChange_IsSpy(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _ID);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _IsSpy);

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
				if (IDInterpolation.Enabled)
				{
					IDInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					IDInterpolation.Timestep = timestep;
				}
				else
				{
					_ID = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_ID(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (IsSpyInterpolation.Enabled)
				{
					IsSpyInterpolation.target = UnityObjectMapper.Instance.Map<bool>(data);
					IsSpyInterpolation.Timestep = timestep;
				}
				else
				{
					_IsSpy = UnityObjectMapper.Instance.Map<bool>(data);
					RunChange_IsSpy(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (IDInterpolation.Enabled && !IDInterpolation.current.UnityNear(IDInterpolation.target, 0.0015f))
			{
				_ID = (int)IDInterpolation.Interpolate();
				//RunChange_ID(IDInterpolation.Timestep);
			}
			if (IsSpyInterpolation.Enabled && !IsSpyInterpolation.current.UnityNear(IsSpyInterpolation.target, 0.0015f))
			{
				_IsSpy = (bool)IsSpyInterpolation.Interpolate();
				//RunChange_IsSpy(IsSpyInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public PlayerNetworkObject() : base() { Initialize(); }
		public PlayerNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public PlayerNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
