using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0,0,0,0,0,0,0]")]
	public partial class GameplayLogicNetworkObject : NetworkObject
	{
		public const int IDENTITY = 4;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private int _numPlayers;
		public event FieldEvent<int> numPlayersChanged;
		public Interpolated<int> numPlayersInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int numPlayers
		{
			get { return _numPlayers; }
			set
			{
				// Don't do anything if the value is the same
				if (_numPlayers == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_numPlayers = value;
				hasDirtyFields = true;
			}
		}

		public void SetnumPlayersDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_numPlayers(ulong timestep)
		{
			if (numPlayersChanged != null) numPlayersChanged(_numPlayers, timestep);
			if (fieldAltered != null) fieldAltered("numPlayers", _numPlayers, timestep);
		}
		[ForgeGeneratedField]
		private bool _finishedTrainsInvisible;
		public event FieldEvent<bool> finishedTrainsInvisibleChanged;
		public Interpolated<bool> finishedTrainsInvisibleInterpolation = new Interpolated<bool>() { LerpT = 0f, Enabled = false };
		public bool finishedTrainsInvisible
		{
			get { return _finishedTrainsInvisible; }
			set
			{
				// Don't do anything if the value is the same
				if (_finishedTrainsInvisible == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_finishedTrainsInvisible = value;
				hasDirtyFields = true;
			}
		}

		public void SetfinishedTrainsInvisibleDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_finishedTrainsInvisible(ulong timestep)
		{
			if (finishedTrainsInvisibleChanged != null) finishedTrainsInvisibleChanged(_finishedTrainsInvisible, timestep);
			if (fieldAltered != null) fieldAltered("finishedTrainsInvisible", _finishedTrainsInvisible, timestep);
		}
		[ForgeGeneratedField]
		private bool _hijackerHasToRespond;
		public event FieldEvent<bool> hijackerHasToRespondChanged;
		public Interpolated<bool> hijackerHasToRespondInterpolation = new Interpolated<bool>() { LerpT = 0f, Enabled = false };
		public bool hijackerHasToRespond
		{
			get { return _hijackerHasToRespond; }
			set
			{
				// Don't do anything if the value is the same
				if (_hijackerHasToRespond == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_hijackerHasToRespond = value;
				hasDirtyFields = true;
			}
		}

		public void SethijackerHasToRespondDirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_hijackerHasToRespond(ulong timestep)
		{
			if (hijackerHasToRespondChanged != null) hijackerHasToRespondChanged(_hijackerHasToRespond, timestep);
			if (fieldAltered != null) fieldAltered("hijackerHasToRespond", _hijackerHasToRespond, timestep);
		}
		[ForgeGeneratedField]
		private bool _finishedTrainsImmutable;
		public event FieldEvent<bool> finishedTrainsImmutableChanged;
		public Interpolated<bool> finishedTrainsImmutableInterpolation = new Interpolated<bool>() { LerpT = 0f, Enabled = false };
		public bool finishedTrainsImmutable
		{
			get { return _finishedTrainsImmutable; }
			set
			{
				// Don't do anything if the value is the same
				if (_finishedTrainsImmutable == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x8;
				_finishedTrainsImmutable = value;
				hasDirtyFields = true;
			}
		}

		public void SetfinishedTrainsImmutableDirty()
		{
			_dirtyFields[0] |= 0x8;
			hasDirtyFields = true;
		}

		private void RunChange_finishedTrainsImmutable(ulong timestep)
		{
			if (finishedTrainsImmutableChanged != null) finishedTrainsImmutableChanged(_finishedTrainsImmutable, timestep);
			if (fieldAltered != null) fieldAltered("finishedTrainsImmutable", _finishedTrainsImmutable, timestep);
		}
		[ForgeGeneratedField]
		private int _numSpies;
		public event FieldEvent<int> numSpiesChanged;
		public Interpolated<int> numSpiesInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int numSpies
		{
			get { return _numSpies; }
			set
			{
				// Don't do anything if the value is the same
				if (_numSpies == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x10;
				_numSpies = value;
				hasDirtyFields = true;
			}
		}

		public void SetnumSpiesDirty()
		{
			_dirtyFields[0] |= 0x10;
			hasDirtyFields = true;
		}

		private void RunChange_numSpies(ulong timestep)
		{
			if (numSpiesChanged != null) numSpiesChanged(_numSpies, timestep);
			if (fieldAltered != null) fieldAltered("numSpies", _numSpies, timestep);
		}
		[ForgeGeneratedField]
		private int _numTrainsAllowed;
		public event FieldEvent<int> numTrainsAllowedChanged;
		public Interpolated<int> numTrainsAllowedInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int numTrainsAllowed
		{
			get { return _numTrainsAllowed; }
			set
			{
				// Don't do anything if the value is the same
				if (_numTrainsAllowed == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x20;
				_numTrainsAllowed = value;
				hasDirtyFields = true;
			}
		}

		public void SetnumTrainsAllowedDirty()
		{
			_dirtyFields[0] |= 0x20;
			hasDirtyFields = true;
		}

		private void RunChange_numTrainsAllowed(ulong timestep)
		{
			if (numTrainsAllowedChanged != null) numTrainsAllowedChanged(_numTrainsAllowed, timestep);
			if (fieldAltered != null) fieldAltered("numTrainsAllowed", _numTrainsAllowed, timestep);
		}
		[ForgeGeneratedField]
		private bool _AllowedToKeepYourUnfinishedTrainsGoing;
		public event FieldEvent<bool> AllowedToKeepYourUnfinishedTrainsGoingChanged;
		public Interpolated<bool> AllowedToKeepYourUnfinishedTrainsGoingInterpolation = new Interpolated<bool>() { LerpT = 0f, Enabled = false };
		public bool AllowedToKeepYourUnfinishedTrainsGoing
		{
			get { return _AllowedToKeepYourUnfinishedTrainsGoing; }
			set
			{
				// Don't do anything if the value is the same
				if (_AllowedToKeepYourUnfinishedTrainsGoing == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x40;
				_AllowedToKeepYourUnfinishedTrainsGoing = value;
				hasDirtyFields = true;
			}
		}

		public void SetAllowedToKeepYourUnfinishedTrainsGoingDirty()
		{
			_dirtyFields[0] |= 0x40;
			hasDirtyFields = true;
		}

		private void RunChange_AllowedToKeepYourUnfinishedTrainsGoing(ulong timestep)
		{
			if (AllowedToKeepYourUnfinishedTrainsGoingChanged != null) AllowedToKeepYourUnfinishedTrainsGoingChanged(_AllowedToKeepYourUnfinishedTrainsGoing, timestep);
			if (fieldAltered != null) fieldAltered("AllowedToKeepYourUnfinishedTrainsGoing", _AllowedToKeepYourUnfinishedTrainsGoing, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			numPlayersInterpolation.current = numPlayersInterpolation.target;
			finishedTrainsInvisibleInterpolation.current = finishedTrainsInvisibleInterpolation.target;
			hijackerHasToRespondInterpolation.current = hijackerHasToRespondInterpolation.target;
			finishedTrainsImmutableInterpolation.current = finishedTrainsImmutableInterpolation.target;
			numSpiesInterpolation.current = numSpiesInterpolation.target;
			numTrainsAllowedInterpolation.current = numTrainsAllowedInterpolation.target;
			AllowedToKeepYourUnfinishedTrainsGoingInterpolation.current = AllowedToKeepYourUnfinishedTrainsGoingInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _numPlayers);
			UnityObjectMapper.Instance.MapBytes(data, _finishedTrainsInvisible);
			UnityObjectMapper.Instance.MapBytes(data, _hijackerHasToRespond);
			UnityObjectMapper.Instance.MapBytes(data, _finishedTrainsImmutable);
			UnityObjectMapper.Instance.MapBytes(data, _numSpies);
			UnityObjectMapper.Instance.MapBytes(data, _numTrainsAllowed);
			UnityObjectMapper.Instance.MapBytes(data, _AllowedToKeepYourUnfinishedTrainsGoing);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_numPlayers = UnityObjectMapper.Instance.Map<int>(payload);
			numPlayersInterpolation.current = _numPlayers;
			numPlayersInterpolation.target = _numPlayers;
			RunChange_numPlayers(timestep);
			_finishedTrainsInvisible = UnityObjectMapper.Instance.Map<bool>(payload);
			finishedTrainsInvisibleInterpolation.current = _finishedTrainsInvisible;
			finishedTrainsInvisibleInterpolation.target = _finishedTrainsInvisible;
			RunChange_finishedTrainsInvisible(timestep);
			_hijackerHasToRespond = UnityObjectMapper.Instance.Map<bool>(payload);
			hijackerHasToRespondInterpolation.current = _hijackerHasToRespond;
			hijackerHasToRespondInterpolation.target = _hijackerHasToRespond;
			RunChange_hijackerHasToRespond(timestep);
			_finishedTrainsImmutable = UnityObjectMapper.Instance.Map<bool>(payload);
			finishedTrainsImmutableInterpolation.current = _finishedTrainsImmutable;
			finishedTrainsImmutableInterpolation.target = _finishedTrainsImmutable;
			RunChange_finishedTrainsImmutable(timestep);
			_numSpies = UnityObjectMapper.Instance.Map<int>(payload);
			numSpiesInterpolation.current = _numSpies;
			numSpiesInterpolation.target = _numSpies;
			RunChange_numSpies(timestep);
			_numTrainsAllowed = UnityObjectMapper.Instance.Map<int>(payload);
			numTrainsAllowedInterpolation.current = _numTrainsAllowed;
			numTrainsAllowedInterpolation.target = _numTrainsAllowed;
			RunChange_numTrainsAllowed(timestep);
			_AllowedToKeepYourUnfinishedTrainsGoing = UnityObjectMapper.Instance.Map<bool>(payload);
			AllowedToKeepYourUnfinishedTrainsGoingInterpolation.current = _AllowedToKeepYourUnfinishedTrainsGoing;
			AllowedToKeepYourUnfinishedTrainsGoingInterpolation.target = _AllowedToKeepYourUnfinishedTrainsGoing;
			RunChange_AllowedToKeepYourUnfinishedTrainsGoing(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _numPlayers);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _finishedTrainsInvisible);
			if ((0x4 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _hijackerHasToRespond);
			if ((0x8 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _finishedTrainsImmutable);
			if ((0x10 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _numSpies);
			if ((0x20 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _numTrainsAllowed);
			if ((0x40 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _AllowedToKeepYourUnfinishedTrainsGoing);

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
				if (numPlayersInterpolation.Enabled)
				{
					numPlayersInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					numPlayersInterpolation.Timestep = timestep;
				}
				else
				{
					_numPlayers = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_numPlayers(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (finishedTrainsInvisibleInterpolation.Enabled)
				{
					finishedTrainsInvisibleInterpolation.target = UnityObjectMapper.Instance.Map<bool>(data);
					finishedTrainsInvisibleInterpolation.Timestep = timestep;
				}
				else
				{
					_finishedTrainsInvisible = UnityObjectMapper.Instance.Map<bool>(data);
					RunChange_finishedTrainsInvisible(timestep);
				}
			}
			if ((0x4 & readDirtyFlags[0]) != 0)
			{
				if (hijackerHasToRespondInterpolation.Enabled)
				{
					hijackerHasToRespondInterpolation.target = UnityObjectMapper.Instance.Map<bool>(data);
					hijackerHasToRespondInterpolation.Timestep = timestep;
				}
				else
				{
					_hijackerHasToRespond = UnityObjectMapper.Instance.Map<bool>(data);
					RunChange_hijackerHasToRespond(timestep);
				}
			}
			if ((0x8 & readDirtyFlags[0]) != 0)
			{
				if (finishedTrainsImmutableInterpolation.Enabled)
				{
					finishedTrainsImmutableInterpolation.target = UnityObjectMapper.Instance.Map<bool>(data);
					finishedTrainsImmutableInterpolation.Timestep = timestep;
				}
				else
				{
					_finishedTrainsImmutable = UnityObjectMapper.Instance.Map<bool>(data);
					RunChange_finishedTrainsImmutable(timestep);
				}
			}
			if ((0x10 & readDirtyFlags[0]) != 0)
			{
				if (numSpiesInterpolation.Enabled)
				{
					numSpiesInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					numSpiesInterpolation.Timestep = timestep;
				}
				else
				{
					_numSpies = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_numSpies(timestep);
				}
			}
			if ((0x20 & readDirtyFlags[0]) != 0)
			{
				if (numTrainsAllowedInterpolation.Enabled)
				{
					numTrainsAllowedInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					numTrainsAllowedInterpolation.Timestep = timestep;
				}
				else
				{
					_numTrainsAllowed = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_numTrainsAllowed(timestep);
				}
			}
			if ((0x40 & readDirtyFlags[0]) != 0)
			{
				if (AllowedToKeepYourUnfinishedTrainsGoingInterpolation.Enabled)
				{
					AllowedToKeepYourUnfinishedTrainsGoingInterpolation.target = UnityObjectMapper.Instance.Map<bool>(data);
					AllowedToKeepYourUnfinishedTrainsGoingInterpolation.Timestep = timestep;
				}
				else
				{
					_AllowedToKeepYourUnfinishedTrainsGoing = UnityObjectMapper.Instance.Map<bool>(data);
					RunChange_AllowedToKeepYourUnfinishedTrainsGoing(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (numPlayersInterpolation.Enabled && !numPlayersInterpolation.current.UnityNear(numPlayersInterpolation.target, 0.0015f))
			{
				_numPlayers = (int)numPlayersInterpolation.Interpolate();
				//RunChange_numPlayers(numPlayersInterpolation.Timestep);
			}
			if (finishedTrainsInvisibleInterpolation.Enabled && !finishedTrainsInvisibleInterpolation.current.UnityNear(finishedTrainsInvisibleInterpolation.target, 0.0015f))
			{
				_finishedTrainsInvisible = (bool)finishedTrainsInvisibleInterpolation.Interpolate();
				//RunChange_finishedTrainsInvisible(finishedTrainsInvisibleInterpolation.Timestep);
			}
			if (hijackerHasToRespondInterpolation.Enabled && !hijackerHasToRespondInterpolation.current.UnityNear(hijackerHasToRespondInterpolation.target, 0.0015f))
			{
				_hijackerHasToRespond = (bool)hijackerHasToRespondInterpolation.Interpolate();
				//RunChange_hijackerHasToRespond(hijackerHasToRespondInterpolation.Timestep);
			}
			if (finishedTrainsImmutableInterpolation.Enabled && !finishedTrainsImmutableInterpolation.current.UnityNear(finishedTrainsImmutableInterpolation.target, 0.0015f))
			{
				_finishedTrainsImmutable = (bool)finishedTrainsImmutableInterpolation.Interpolate();
				//RunChange_finishedTrainsImmutable(finishedTrainsImmutableInterpolation.Timestep);
			}
			if (numSpiesInterpolation.Enabled && !numSpiesInterpolation.current.UnityNear(numSpiesInterpolation.target, 0.0015f))
			{
				_numSpies = (int)numSpiesInterpolation.Interpolate();
				//RunChange_numSpies(numSpiesInterpolation.Timestep);
			}
			if (numTrainsAllowedInterpolation.Enabled && !numTrainsAllowedInterpolation.current.UnityNear(numTrainsAllowedInterpolation.target, 0.0015f))
			{
				_numTrainsAllowed = (int)numTrainsAllowedInterpolation.Interpolate();
				//RunChange_numTrainsAllowed(numTrainsAllowedInterpolation.Timestep);
			}
			if (AllowedToKeepYourUnfinishedTrainsGoingInterpolation.Enabled && !AllowedToKeepYourUnfinishedTrainsGoingInterpolation.current.UnityNear(AllowedToKeepYourUnfinishedTrainsGoingInterpolation.target, 0.0015f))
			{
				_AllowedToKeepYourUnfinishedTrainsGoing = (bool)AllowedToKeepYourUnfinishedTrainsGoingInterpolation.Interpolate();
				//RunChange_AllowedToKeepYourUnfinishedTrainsGoing(AllowedToKeepYourUnfinishedTrainsGoingInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public GameplayLogicNetworkObject() : base() { Initialize(); }
		public GameplayLogicNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public GameplayLogicNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
