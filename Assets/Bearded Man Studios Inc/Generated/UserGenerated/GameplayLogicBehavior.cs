using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedRPC("{\"types\":[[\"int\", \"int\", \"int\", \"string\", \"string\", \"int\"][\"int\"][]]")]
	[GeneratedRPCVariableNames("{\"types\":[[\"OriginPlayer\", \"DestinationPlayer\", \"RespondedPlayer\", \"FirstString\", \"SecondString\", \"Response\"][\"Character\"][]]")]
	public abstract partial class GameplayLogicBehavior : NetworkBehavior
	{
		public const byte RPC_LAUNCH_MESSAGE = 0 + 5;
		public const byte RPC_MAKE_GUESS = 1 + 5;
		public const byte RPC_START_GAME = 2 + 5;
		
		public GameplayLogicNetworkObject networkObject = null;

		public override void Initialize(NetworkObject obj)
		{
			// We have already initialized this object
			if (networkObject != null && networkObject.AttachedBehavior != null)
				return;
			
			networkObject = (GameplayLogicNetworkObject)obj;
			networkObject.AttachedBehavior = this;

			base.SetupHelperRpcs(networkObject);
			networkObject.RegisterRpc("LaunchMessage", LaunchMessage, typeof(int), typeof(int), typeof(int), typeof(string), typeof(string), typeof(int));
			networkObject.RegisterRpc("MakeGuess", MakeGuess, typeof(int));
			networkObject.RegisterRpc("StartGame", StartGame);

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
			Initialize(new GameplayLogicNetworkObject(networker, createCode: TempAttachCode, metadata: metadata));
		}

		private void DestroyGameObject(NetWorker sender)
		{
			MainThreadManager.Run(() => { try { Destroy(gameObject); } catch { } });
			networkObject.onDestroy -= DestroyGameObject;
		}

		public override NetworkObject CreateNetworkObject(NetWorker networker, int createCode, byte[] metadata = null)
		{
			return new GameplayLogicNetworkObject(networker, this, createCode, metadata);
		}

		protected override void InitializedTransform()
		{
			networkObject.SnapInterpolations();
		}

		/// <summary>
		/// Arguments:
		/// int OriginPlayer
		/// int DestinationPlayer
		/// int RespondedPlayer
		/// string FirstString
		/// string SecondString
		/// int Response
		/// </summary>
		public abstract void LaunchMessage(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// int Character
		/// </summary>
		public abstract void MakeGuess(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// </summary>
		public abstract void StartGame(RpcArgs args);

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}