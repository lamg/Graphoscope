(**
---
title: Dijkstra
category: Algorithms 
categoryindex: 2
index: 1
---
*)


(*** hide ***)

(*** condition: prepare ***)
#r "nuget: FSharpAux.Core, 2.0.0"
#r "nuget: FSharpAux.IO, 2.0.0"
#r "nuget: Cytoscape.NET, 0.2.0"
#r "../src/Graphoscope/bin/Release/netstandard2.0/Graphoscope.dll"

(*** condition: ipynb ***)
#if IPYNB
#r "nuget: Graphoscope, {{fsdocs-package-version}}"
#endif // IPYNB

(**
# Shortest path between all the vertices using Dijkstra�a Algorithm.

Dijkstra�s algorithm, given by a brilliant Dutch computer scientist and software engineer Dr. Edsger Dijkstra in 1959. 
Dijkstra�s algorithm is a greedy algorithm that solves the single-source shortest path problem for a directed and undirected
graph that has non-negative edge weight.

Let's start with a directed weighted graph. We will find shortest path between all the vertices using Dijkstra�a Algorithm.

*)

open Graphoscope

let dwg =
    FGraph.empty
    |> FGraph.addNode 0 "A"
    |> FGraph.addNode 1 "B"
    |> FGraph.addNode 2 "C"
    |> FGraph.addNode 3 "D"
    |> FGraph.addNode 4 "E"
    |> FGraph.addNode 5 "F"
    |> FGraph.addEdge 0 1 7.
    |> FGraph.addEdge 0 2 12.
    |> FGraph.addEdge 1 2 2.
    |> FGraph.addEdge 1 3 9.
    |> FGraph.addEdge 2 4 10.
    |> FGraph.addEdge 4 3 4.
    |> FGraph.addEdge 3 5 1.
    |> FGraph.addEdge 4 5 5.

let dij = Algorithms.Dijkstra.ofFGraph 0 dwg




open Cytoscape.NET

let vizGraph =
    CyGraph.initEmpty ()
    |> CyGraph.withElements [
            for (sk,s,tk,t,el) in (FGraph.toSeq dwg) do
                let sk, tk = (string sk), (string tk)
                yield Elements.node sk [ CyParam.label s ]
                yield Elements.node tk [ CyParam.label t ]
                yield Elements.edge  (sprintf "%s_%s" sk tk) sk tk [ CyParam.label el ]
        ]
    |> CyGraph.withStyle "node"     
        [
            CyParam.content =. CyParam.label
            CyParam.color "#A00975"
        ]
    |> CyGraph.withStyle "edge"     
        [
            CyParam.content =. CyParam.label
            CyParam.Curve.style "bezier"
            CyParam.Target.Arrow.shape "triangle"
            CyParam.Source.Arrow.shape "circle"
            CyParam.color "#438AFE"
        ]
(***hide***)
vizGraph 
|> CyGraph.withZoom(CytoscapeModel.Zoom.Init(ZoomingEnabled=false)) 
    |> CyGraph.withLayout (
        Layout.initCose (Layout.LayoutOptions.Cose(ComponentSpacing=40)) 
        ) 
|> CyGraph.withSize(800, 400) 
|> CyGraph.show
//|> HTML.toGraphHTML()
 
(*** include-it-raw ***)
