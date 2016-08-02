/***********************
 *  CUSTOM TEMPLATES   *
 ***********************/
//#5353ac", "#008fb5", "#f1c109"

var kelly_colors = [
    "#817066", // Medium Gray
    "#803E75", // Strong Purple
    "#C10020", // Vivid Red
    "#CEA262", // Grayish Yellow
    "#FF6800", // Vivid Orange
    "#FFB300", // Vivid Yellow
    "#A6BDD7", // Very Light Blue
    
    
    

    // The following don't work well for people with defective color vision
    "#007D34", // Vivid Green
    "#F6768E", // Strong Purplish Pink
    "#00538A", // Strong Blue
    "#FF7A5C", // Strong Yellowish Pink
    "#53377A", // Strong Violet
    "#FF8E00", // Vivid Orange Yellow
    "#B32851", // Strong Purplish Red
    "#F4C800", // Vivid Greenish Yellow
    "#7F180D", // Strong Reddish Brown
    "#93AA00", // Vivid Yellowish Green
    "#593315", // Deep Yellowish Brown
    "#F13A13", // Vivid Reddish Orange
    "#232C16", // Dark Olive Green
]

var last_kelly = 0;
var diffMode = false;

var myTemplateConfig = {
    arrow: {
        size: 10,
        offset: -3
    },
    colors: kelly_colors,
    branch: {
        lineWidth: 5,
        spacingX: 16
    },
    commit: {
        spacingY: -30,
        dot: {
            size: 6
        },
        message: {
            display: true,
            displayAuthor: false,
            displayBranch: false,
            displayHash: false,
            font: "normal 12pt Calibri",
            color: "black"
        },
        tooltipHTMLFormatter: function (commit) {
            return "" + commit.realMessage;
        }
    }
};
var myTemplate = new GitGraph.Template(myTemplateConfig);

var branches = new Object();
var commits = new Object();

/***********************
 *    INITIALIZATION   *
 ***********************/

var gitGraph = new GitGraph();

gitGraph.template = myTemplate;

branches["master"] = [
    gitGraph.branch("master"), last_kelly++];

var selectedCommit;

/***********************
 *       EVENTS        *
 ***********************/

gitGraph.canvas.addEventListener("commit:mouseover", function (event) {
    console.log("You're over a commit.", "Here is a bunch of data ->", event.data);
});

// Attach a handler to the commit

function comTouch(commit, restore) {
    console.log("You just clicked my commit. - " + window.diffMode, commit);

    if (window.diffMode === false) {
        commit.dotColor = "white";
        commit.dotStrokeWidth = 10;

        if (selectedCommit != null && commit != selectedCommit) {
            selectedCommit.dotColor = selectedCommit.dotStrokeColor;
            selectedCommit.dotStrokeWidth = null;
            selectedCommit.parent.render();

            /*if (commit.changes != null) {
                var x = -1;
                var y = -1;
                var pathA = "";
                var pathB = "";
                var commitA = selectedCommit;
                var commitB = commit;
                var final;
    
                console.log("new" + commit.changes);
                console.log("old" + selectedCommit.changes);
                
                while (commitA != commitB) {
                    if (commitA.y > commitB.y) {
                        pathA = pathA.concat("R|"+commitA.changes+"***");
                        commitA = commitA.parentCommit;
                    } else {
                        pathB = pathB.concat("A|"+commitB.changes+"***");
                        commitB = commitB.parentCommit;
                    }
                }
                final = pathA.concat(pathB);
                console.log(final);*/
            if (restore) window.external.RestoreCommit(commit.sha1);
        }
        selectedCommit = commit;

        commit.parent.render();
    } else {
        //Begin diffing
        window.external.BeginDiff(commit.sha1);
    }

}

function toggleDiff() {
    $("#diffButton").click();
}

function refreshLog(json) {
    if (window.diffMode === true) {
        this.toggleDiff();
    }
    var parsed = JSON.parse(json);
    var master = branches["master"][0];
    var finalCommit;
    var i = 1;
    //debugger;
    for (var pindex in parsed) {
        var key = Object.keys(parsed[pindex])[0];
        //if (parsed.hasOwnProperty(key)) {
            //if(key === "head") continue;
            if (key === "reset") {
                gitGraph = new GitGraph();
                last_kelly = 0;
                gitGraph.template = myTemplate;
                branches = new Object();
                commits = new Object();
                branches["master"] = [
                    gitGraph.branch("master"), last_kelly++];
                continue;
            }
            var entry = parsed[pindex][key];
            /*if (first && commits["latest"] != null) {
                commits["latest"].sha1 = key;
                commits[entry["parent"]] = commits["latest"];
                first = false;
                continue;
            }*/

            var parent = entry["parent"];
            //<b>Commit message</b><br><i>Cell C28 = 15/09/2001</i>"
            if (branches[entry["branch"]] == null) {
                branches[entry["branch"]] = [gitGraph.branch({ parentCommit: commits[entry["parent"]], name: entry["branch"]}), last_kelly];
            }

            var br = entry["branch"];
            var auth = entry["author"];
            var brr = entry.branch;

            if (commits[key] == null) {
                branches[entry["branch"]][0].commit({
                    realMessage: "<b>" + entry["message"] + "</b><br><i>" + entry["branch"] + " <" + "e" + "><br>" + "d" + "</i>",
                    message: "V" + (Object.keys(commits).length + 1),
                    onClick: function (commit) { comTouch(commit, true); },
                    changes: "Placeholder for changes",
                    sha1: key,
                    dotColor: kelly_colors[branches[entry["branch"]][1]]
                });

                commits[key] = branches[entry["branch"]][0].commits.slice(-1)[0];
                finalCommit = commits[key];
            }


            i++;
        //}
    }
    var com = finalCommit;
    comTouch(com, false);
    selectedCommit.parent.render();
}



$("button").click(function () {
    if ($(this).text() == 'Comparison Mode') {
        $(this).html('Normal Mode');
        $('body').css({ 'background': "#cdc3c3" });
        diffMode = true;
    } else {
        $(this).html('Comparison Mode');
        $('body').css({ 'background': "#EEE" });
        diffMode = false;
    }
});

