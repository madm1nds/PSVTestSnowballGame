using System;
using UnityEngine;


public static class GroundCoordinates
{
    public static float correctionBottom;
    public static float correctionTop;

    public static void SetGround(GameObject groundBottom, GameObject groundTop, GameObject groundLeft, GameObject groundRight, GameObject player, Camera cam)
    {
        CorrectBottomСoordinates(cam);

        //вычитаем координаты объекта и камеры, чтобы найти координаты растояния в Vector3 
        Vector3 cameraToObject = player.transform.position - cam.transform.position;
        //Дистанция от камеры до 2d плоскости. Преобразуем расстояние из Vector3 в обычный float        
        float distance = Vector3.Project(cameraToObject, cam.transform.forward).z;


        // вершины "среза" пирамиды видимости камеры на необходимом расстоянии от камеры
        Vector3 leftBot = cam.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightTop = cam.ViewportToWorldPoint(new Vector3(1, 1, distance));

        // переводим границы в плоскости в нормальный вид XY координат 
        //float x_left = leftBot.x;
        //float x_right = rightTop.x;
        float y_top = rightTop.y;
        float y_bot = leftBot.y;


        groundTop.transform.position = new Vector3(groundTop.transform.position.x, y_top + correctionTop, groundTop.transform.position.z);

        groundBottom.transform.position = new Vector3(groundBottom.transform.position.x, y_bot + correctionBottom, groundBottom.transform.position.z);


    }

    private static void CorrectBottomСoordinates(Camera cam)
    {
        // 131 это граница, где нужно прибавлять ещё больше коррекции
        if (cam.fieldOfView < 131)
        {
            // Если значение меньше 95, то тоже надо прибавлять больше коррекции.
            if (cam.fieldOfView >= 95)
            {
                correctionBottom = cam.fieldOfView * 0.15f;
            }
            else
            {
                correctionBottom = cam.fieldOfView * ((Math.Abs(cam.fieldOfView - 95) / 500) + 0.15f);
            }            
        }
        else
        {
            correctionBottom = cam.fieldOfView * ((Math.Abs(cam.fieldOfView - 131) / 500) + 0.15f);
        }
                
        correctionTop = Math.Abs(correctionBottom); correctionTop -= 26;
        // Коррекция для нижнего барьера всегда должна быть отрицательной
        correctionBottom = -Math.Abs(correctionBottom); correctionBottom += 10;      
    }
}
