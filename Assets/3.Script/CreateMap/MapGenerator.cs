using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

// using System;

public class MapGenerator : MonoBehaviour 
{
    [SerializeField] private RuleTile rulePaper;
    [SerializeField] private RuleTile ruleEndTile;
    [SerializeField] public RuleTile ruleSoil;
    [SerializeField] public RuleTile ruleStone;
    [SerializeField] private RuleTile ruleIron;
    [SerializeField] private RuleTile ruleCopper;
    [SerializeField] private RuleTile ruleGold;
    [SerializeField] private RuleTile ruleTungsten;
    [SerializeField] private RuleTile ruleOrichalcum;
    [SerializeField] private Tilemap blockTileMap;
    [SerializeField] private Tilemap wallPaper;
    [SerializeField] private Tilemap endTileMap;


    public int width;
	public int height;
	


	public string seed;
	public bool useRandomSeed;

	[Range(0,100)]
	public int randomFillPercent;

	int[,] map;


	void Start() 
	{
		GenerateMap();
        CreateWallPaper();
		DrawTileMap();

    }


	//지정한 범위 의 맵안에 0과1로가득채운다
    void GenerateMap() {
		map = new int[width,height];
		RandomFillMap();
		//이 for문 반복횟수에따라 맵이 좀더 매끄러워짐 알고리즘을 적용하는 부분 
		for (int i = 0; i < 5; i ++) {
			SmoothMap();
		}
	}


	//플레이어가 설정한 채우는 %에 따라서 맵을채운다
	void RandomFillMap() {
		if (useRandomSeed) {
			seed = Time.time.ToString();
		}

		for (int x = 0; x < width; x ++) {
			for (int y = 0; y < height; y ++) {
				if (x == 0 || x == width-1 || y == 0 || y == height -1) {
					map[x,y] = 1;
				}
				else {
					map[x,y] = (Random.Range(0,100) < randomFillPercent)? 1: 0;
				}
			}
		}
	}

	//주변의 빈공간이 몇개인지에 따라서 해당 2차원배열의 값을 변경함
	void SmoothMap() {
		for (int x = 0; x < width; x ++) {
			for (int y = 0; y < height; y ++) {
				int neighbourWallTiles = GetSurroundingWallCount(x,y);

				if (neighbourWallTiles > 4)
					map[x,y] = 1;
				else if (neighbourWallTiles < 4)
					map[x,y] = 0;

			}
		}
	}

	//지정위치 근처 팔방면에 벽이 있는지 없는지를 구하는 수식 
	int GetSurroundingWallCount(int gridX, int gridY) {
		int wallCount = 0;
		for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX ++) {
			for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY ++) {
				if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height) {
					if (neighbourX != gridX || neighbourY != gridY) {
						wallCount += map[neighbourX,neighbourY];
					}
				}
				else {
					wallCount ++;
				}
			}
		}

		return wallCount;
	}

	//맵 전체에 흙배경을 깔기위한메소드
    public void CreateWallPaper()
    {
		if (map != null)
		{
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					Vector3 pos = new Vector2(-width / 2 + x, -height / 2 + y);
					Vector3Int tilePos = new Vector3Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));
					wallPaper.SetTile(tilePos, rulePaper);

				}
			}
		}
	}
    

	//y값에 따라 다르게 타일맵을 입력하여 층을 이루게 구현
    public void DrawTileMap()
    {
        if (map != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vector3 pos = new Vector2(-width / 2 + x, -height / 2 + y);
                    Vector3Int tilePos = new Vector3Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));

					if (map[x, y] == 1)
                    {
						if (x == 0 || y == 0 || x == width - 1)
                        {
							endTileMap.SetTile(tilePos, ruleEndTile);
							continue;
                        }
                        
						if(y > height * 0.7)
                        {
							blockTileMap.SetTile(tilePos, ruleSoil);
						}
						else if (y <= height * 0.7 && y > height * 0.55)
                        {
							blockTileMap.SetTile(tilePos, ruleStone);
                        }
						else if (y >= height * 0.4 && y <= height * 0.55)
						{
							blockTileMap.SetTile(tilePos, ruleCopper);
						}
						else if (y <= height * 0.4 && y > height * 0.30)
						{
							blockTileMap.SetTile(tilePos, ruleIron);
						}
						else if (y <= height * 0.30 && y > height * 0.20)
						{
							blockTileMap.SetTile(tilePos, ruleTungsten);
						}
						else if (y <= height * 0.20 && y > height * 0.10)
						{
							blockTileMap.SetTile(tilePos, ruleGold);
						}
						else
						{
							blockTileMap.SetTile(tilePos, ruleOrichalcum);
						}

					}
                }
            }
        }
    }

}
