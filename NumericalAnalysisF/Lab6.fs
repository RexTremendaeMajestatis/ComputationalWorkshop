#light
module NumericalAnalysisF.Lab6

open System

let pow n x = 
    Math.Pow(x, n)

let w x = 
    Math.Pow(x, -0.25)

let simpson a b m f = 
    let h = (b - a) / (float m * 2.0)
    
    let rec loop a h i m f acc =
        if i = 2 * m then 
            acc
        else
            if i % 2 = 0 then
                let x = a + float i * h
                let s = f(x)
                loop a h (i + 1) m f (acc + 2.0 * s)
            else
                let x = a + float i * h
                let s = f(x)
                loop a h (i + 1) m f (acc + 4.0 * s)
        
    (h / 3.0) * loop a h 1 m f (f(a) + f(b))

let multIntegral a b m w f = 
    let eps = 0.000001
    let h = (b - a) / (float m * 2.0)
    
    let rec loop a h i m w f acc =
        if i = 2 * m then 
            acc
        else
            if i % 2 = 0 then
                let x = a + float i * h
                let s = (f x) * (w x)
                loop a h (i + 1) m w f (acc + 2.0 * s)
            else
                let x = a + float i * h
                let s = (f x) * (w x)
                loop a h (i + 1) m w f (acc + 4.0 * s)
        
    (h / 3.0) * loop a h 1 m w f (f(a + eps) * w(a + eps) + f(b) * w(b))

let moment a b k w =
    let m = int ((b - a) / 0.00001)

    multIntegral a b m (pow <| k) w
