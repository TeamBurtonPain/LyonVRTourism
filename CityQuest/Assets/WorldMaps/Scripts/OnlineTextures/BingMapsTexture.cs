using UnityEngine;
using System.Collections;
using System;
using System.Text;


public class BingMapsTexture : OnlineTexture {
    public static string testServerURL = "http://ecn.{subdomain}.tiles.virtualearth.net/tiles/r{quadkey}.jpeg?g=6422&mkt={culture}&shading=hill&dpi=Large";
    //"http://ecn.{subdomain}.tiles.virtualearth.net/tiles/r{quadkey}.jpeg?g=4892&mkt={culture}&shading=hill";
    public string serverURL = BingMapsTexture.testServerURL;
	public string initialSector = "0";
	public float latitude = 28.127222f;
	public float longitude = -15.431389f;
	public int initialZoom = 0;
    public Vector2 origin;

    private new void Start()
    {
        ComputeInitialSector();
    }

    public void ComputeInitialSector()
	{
		float sinLatitude = Mathf.Sin (latitude * Mathf.PI / 180.0f);

		int pixelX = (int)( ((longitude + 180) / 360) * 256 * Mathf.Pow (2, initialZoom + 1) );
		int pixelY = (int)( (0.5f - Mathf.Log ((1 + sinLatitude) / (1 - sinLatitude)) / (4 * Mathf.PI)) * 256 * Mathf.Pow (2, initialZoom + 1) );

		int tileX = Mathf.FloorToInt (pixelX / 256);
		int tileY = Mathf.FloorToInt (pixelY / 256);


        // Explications ? hahahaha hahaha
        // haha... j'ai pleuré... bon, en gros, Bing coupe cest map en faisant le cast + "floor" (partie entiere) juste au dessus... donc pour 
        // retrouver le coin de la map, bah on reprends la valeur arrondie, on applique les calculs dans l'autre sens et on prie, et hop, coord de l'origin de la map !

        float tempX = (0.5f - tileY / Mathf.Pow(2, initialZoom + 1)) * (4 * Mathf.PI);
        float latOrigin = Mathf.Asin((Mathf.Exp(tempX) - 1) / (Mathf.Exp(tempX) + 1)) / Mathf.PI * 180.0f;
        float longOrigin = tileX / Mathf.Pow(2, initialZoom + 1) * 360 - 180;
        this.origin = new Vector2(latOrigin, longOrigin);

        initialSector = TileXYToQuadKey (tileX, tileY, initialZoom + 1);
	}


	// Function taken from "Bing Maps Tile System": https://msdn.microsoft.com/en-us/library/bb259689.aspx
	public static string TileXYToQuadKey(int tileX, int tileY, int levelOfDetail)
	{
		StringBuilder quadKey = new StringBuilder();
		for (int i = levelOfDetail; i > 0; i--)
		{
			char digit = '0';
			int mask = 1 << (i - 1);
			if ((tileX & mask) != 0)
			{
				digit++;
			}
			if ((tileY & mask) != 0)
			{
				digit++;
				digit++;
			}
			quadKey.Append(digit);
		}
		return quadKey.ToString();
	}


	public static bool ValidateServerURL(string serverURL, out string errorMessage)
	{
		errorMessage = "";
		if( serverURL.IndexOf("{quadkey}" ) < 0 ){
			errorMessage = "BingMaps inspector - missing {quadkey} in server URL";
			return false;
		}
		if( serverURL.IndexOf("{subdomain}" ) < 0 ){
			errorMessage = "BingMaps inspector - missing {subdomain} in server URL";
			return false;
		}
		return true;
	}


	protected override string GenerateRequestURL( string nodeID )
	{
		// Children node numbering differs between QuadtreeLODNoDe and Bing maps, so we
		// correct it here.
		nodeID = nodeID.Substring(1).Replace('1','9').Replace('2','1').Replace('9','2');

		string url = CurrentFixedUrl ();
		url = url.Replace ("{quadkey}", initialSector + nodeID);
		url = url.Replace ("{subdomain}", "t0");
		return url;
	}


	public string CurrentFixedUrl ()
	{
		return serverURL;
	}


	protected override void InnerCopyTo(OnlineTexture copy)
	{
		BingMapsTexture target = (BingMapsTexture)copy;
		target.serverURL = serverURL;
		target.initialSector = initialSector;
		target.latitude = latitude;
		target.longitude = longitude;
		target.initialZoom = initialZoom;
	}
}
