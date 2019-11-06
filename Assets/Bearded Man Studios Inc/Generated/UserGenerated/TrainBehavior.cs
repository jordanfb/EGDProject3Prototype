using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedRPC("{\"types\":[[\"string\", \"string\", \"int\", \"int\"][\"int\", \"string\"][][]]")]
	[GeneratedRPCVariableNames("{\"types\":[[\"Choice1\", \"Choice2\", \"From\", \"To\"][\"Responder\", \"Response\"][][]]")]
	public abstract partial class TrainBehavior : NetworkBehavior
	{
		public const byte RPC_SET_CHOICES = 0 + 5;
		public const byte RPC_SET_RESPONSE = 1 + 5;
		public const byte RPC_STOP_TRAIN = 2 + 5;
		public const byte RPC_START_TRAIN = 3 + 5;
		
		public TrainNetworkObject networkObject = null;

		public override void Initialize(NetworkObject obj)
		{
			// We have already initialized this object
			if (networkObject != null && networkObject.AttachedBehavior != null)
				return;
			
			networkObject = (TrainNetworkObject)obj;
			networkObject.AttachedBehavior = this;

			base.SetupHelperRpcs(networkObject);
			networkObject.RegisterRpc("SetChoices", SetChoices, typeof(string), typeof(string), typeof(int), typeof(int));
			networkObject.RegisterRpc("SetResponse", SetResponse, typeof(int), typeof(string));
			networkObject.RegisterRpc("StopTrain", StopTrain);
			networkObject.RegisterRpc("StartTrain", StartTrain);

			networkObject.onDestroy += DestroyGameObject;

			if (!obj.IsOwner)
			{
				if (!skipAttachIds.ContainsKey(obj.NetworkId)){
					uint newId = obj.NetworkId + 1;
					ProcessOthers(gameObject.transform, ref newId);
				}
				else
					skipAttachIds.Remove(obj.NetworkId);
			}

			if (obj.Metadata != null)
			{
				byte transformFlags = obj.Metadata[0];

				if (transformFlags != 0)
				{
					BMSByte metadataTransform = new BMSByte();
					metadataTransform.Clone(obj.Metadata);
					metadataTransform.MoveStartIndex(1);

					if ((transformFlags & 0x01) != 0 && (transformFlags & 0x02) != 0)
					{
						MainThreadManager.Run(() =>
						{
							transform.position = ObjectMapper.Instance.Map<Vector3>(metadataTransform);
							transform.rotation = ObjectMapper.Instance.Map<Quaternion>(metadataTransform);
						});
					}
					else if ((transformFlags & 0x01) != 0)
					{
						MainThreadManager.Run(() => { transform.position = ObjectMapper.Instance.Map<Vector3>(metadataTransform); });
					}
					else if ((transformFlags & 0x02) != 0)
					{
						MainThreadManager.Run(() => { transform.rotation = ObjectMapper.Instance.Map<Quaternion>(metadataTransform); });
					}
				}
			}

			MainThreadManager.Run(() =>
			{
				NetworkStart();
				networkObject.Networker.FlushCreateActions(networkObject);
			});
		}

		protected override void CompleteRegistration()
		{
			base.CompleteRegistration();
			networkObject.ReleaseCreateBuffer();
		}

		public override void Initialize(NetWorker networker, byte[] metadata = null)
		{
			Initialize(new TrainNetworkObject(networker, createCode: TempAttachCode, metadata: metadata));
		}

		private void DestroyGameObject(NetWorker sender)
		{
			MainThreadManager.Run(() => { try { Destroy(gameObject); } catch { } });
			networkObject.onDestroy -= DestroyGameObject;
		}

		public override NetworkObject CreateNetworkObject(NetWorker networker, int createCode, byte[] metadata = null)
		{
			return new TrainNetworkObject(networker, this, createCode, metadata);
		}

		protected override void InitializedTransform()
		{
			networkObject.SnapInterpolations();
		}

		/// <summary>
		/// Arguments:
		/// string Choice1
		/// string Choice2
		/// int From
		/// int To
		/// </summary>
		public abstract void SetChoices(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// int Responder
		/// string Response
		/// </summary>
		public abstract void SetResponse(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// </summary>
		public abstract void StopTrain(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// </summary>
		public abstract void StartTrain(RpcArgs args);

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}