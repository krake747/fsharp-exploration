open Plotly.NET
open Plotly.NET.TraceObjects

let xData = [ 0. .. 10. ]
let yData = [ 0. .. 10. ]

let myFirstChart =
    Chart.Point(xData, yData)
    |> Chart.withTitle "Hello world!"
    |> Chart.withXAxisStyle ("xAxis")
    |> Chart.withYAxisStyle ("yAxis")

// myFirstChart |> Chart.show

let stackedBar =
    let values = [ 50; 30; 10; 10;  ]
    let keys = [ "NVDA"; "AMD"; "SIE"; "MSFT" ]
    Chart.Column(values = values, Keys = keys, Name = "Weights")

// stackedBar |> Chart.show

let treemapStyled =
    let labelsParents = [
        ("NVDA", ""), 50
        ("AMD", ""), 30; ("SIE", ""), 10; ("MSFT", ""), 10
    ]

    Chart.Treemap(
        labelsparents = (labelsParents |> Seq.map fst),
        Values = (labelsParents |> Seq.map snd),
        BranchValues = StyleParam.BranchValues.Total,
        SectionColorScale = StyleParam.Colorscale.Greens, 
        ShowSectionColorScale = true,
        SectionOutlineColor = Color.fromKeyword Black,
        Tiling = TreemapTiling.init (Packing = StyleParam.TreemapTilingPacking.Squarify)
    )

treemapStyled |> Chart.show
