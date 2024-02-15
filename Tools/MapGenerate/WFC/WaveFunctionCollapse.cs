using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using FEngine;

public class WaveFunctionCollapse : MonoBehaviour
{
    public int gridSize = 3; // 网格大小
    public GameObject[] prefabs; // 所有可用游戏对象

    private int[,] inputArray = {
        { 1, 2, 1 },
        { 2, 1, 2 },
        { 1, 2, 1 }
    }; // 输入数组（规定的图案）

    private int[,] outputArray; // 输出数组

    private List<int[,]> patterns; // 可用模式列表

    private List<GameObject>[,] spawnedObjects; // 已生成的游戏对象列表
    
    
    
    private void Start()
    {
        outputArray = new int[gridSize, gridSize]; // 初始化输出数组
        spawnedObjects = new List<GameObject>[gridSize, gridSize]; // 初始化已生成的游戏对象列表

        for (int i = 0; i < gridSize; i++) // 遍历网格行
        {
            for (int j = 0; j < gridSize; j++) // 遍历网格列
            {
                spawnedObjects[i, j] = new List<GameObject>(); // 为每个单元格初始化游戏对象列表
            }
        }

        patterns = GetAllPatterns(inputArray); // 获取所有可用模式

        while (true)
        {
            int row = -1;
            int col = -1;

            for (int i = 0; i < gridSize; i++) // 遍历网格行
            {
                for (int j = 0; j < gridSize; j++) // 遍历网格列
                {
                    if (outputArray[i, j] == 0) // 如果该单元格尚未填充
                    {
                        row = i;
                        col = j;
                        break;
                    }
                }

                if (row != -1)
                {
                    break;
                }
            }

            if (row == -1) // 如果所有单元格都已填满，退出循环
            {
                break;
            }

            List<int[,]> compatiblePatterns = GetCompatiblePatterns(patterns, outputArray, row, col); // 获取与当前位置兼容的所有可用模式

            if (compatiblePatterns.Count == 0)
            {
                throw new System.Exception("No compatible patterns found"); // 如果没有可用的模式，抛出异常
            }

            int[,] pattern = PickRandomPattern(compatiblePatterns); // 随机选择一个可用模式

            for (int i = 0; i < pattern.GetLength(0); i++) // 遍历模式中的行
            {
                for (int j = 0; j < pattern.GetLength(1); j++) // 遍历模式中的列
                {
                    outputArray[row + i, col + j] = pattern[i, j]; // 将模式拷贝到输出数组中对应位置

                    foreach (GameObject prefab in prefabs) // 遍历所有可用游戏对象
                    {
                        if (prefab.GetComponent<ObjectData>().id == pattern[i, j]) // 如果游戏对象与当前数字匹配
                        {
                            GameObject newObj = Instantiate(prefab, new Vector3(col + j - (gridSize / 2), 0, -(row + i - (gridSize / 2))), Quaternion.identity); // 在场景中生成游戏对象
                            spawnedObjects[row + i, col + j].Add(newObj); // 将游戏对象添加到已生成列表中
                        }
                    }
                }
            }
        }
    }

    private List<int[,]> GetAllPatterns(int[,] input)
    {
        List<int[,]> patterns = new List<int[,]>();

        for (int i = 0; i < input.GetLength(0) - 1; i++) // 遍历输入数组的行
        {
            for (int j = 0; j < input.GetLength(1) - 1; j++) // 遍历输入数组的列
            {
                int[,] pattern = new int[2, 2];

                pattern[0, 0] = input[i, j];
                pattern[0, 1] = input[i, j + 1];
                pattern[1, 0] = input[i + 1, j];
                pattern[1, 1] = input[i + 1, j + 1];

                patterns.Add(pattern); // 将构建的模式添加到列表中
            }
        }

        return patterns;
    }

    private List<int[,]> GetCompatiblePatterns(List<int[,]> patterns, int[,] output, int row, int col)
    {
        List<int[,]> compatiblePatterns = new List<int[,]>();

        foreach (int[,] pattern in patterns) // 遍历所有可用模式
        {
            if (IsPatternCompatible(pattern, output, row, col)) // 如果模式与输出数组在当前位置兼容
            {
                compatiblePatterns.Add(pattern); // 将模式添加到兼容模式列表中
            }
        }

        return compatiblePatterns;
    }

    private bool IsPatternCompatible(int[,] pattern, int[,] output, int row, int col)
    {
        if (row + pattern.GetLength(0) > output.GetLength(0) || col + pattern.GetLength(1) > output.GetLength(1)) // 如果当前位置无法放置整个模式，返回false
        {
            return false;
        }

        for (int i = 0; i < pattern.GetLength(0); i++) // 遍历模式中的行
        {
            for (int j = 0; j < pattern.GetLength(1); j++) // 遍历模式中的列
            {
                if (output[row + i, col + j] != 0 && output[row + i, col + j] != pattern[i, j]) // 如果输出数组中该位置已经被填充，并且与模式中的数字不匹配
                {
                    return false; // 返回false
                }
            }
        }

        return true;
    }

    private int[,] PickRandomPattern(List<int[,]> patterns)
    {
        return patterns[Random.Range(0, patterns.Count)]; // 从可用模式列表中随机选择一个
    }
}