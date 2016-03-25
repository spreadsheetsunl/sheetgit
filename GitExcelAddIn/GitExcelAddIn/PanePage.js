/***********************
 *  CUSTOM TEMPLATES   *
 ***********************/
//#5353ac", "#008fb5", "#f1c109"
var myTemplateConfig = {
    colors: ["#676767", "#D43A3A", "#3A62D4"],
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
            display: false,
            displayAuthor: false,
            displayBranch: false,
            displayHash: false,
            font: "normal 12pt Calibri",
            color: "black"
        },
        tooltipHTMLFormatter: function (commit) {
            return "" + commit.message;
        }
    }
};
var myTemplate = new GitGraph.Template(myTemplateConfig);

/***********************
 *    INITIALIZATION   *
 ***********************/

var config = {
    template: "metro"       // could be: "blackarrow" or "metro" or `myTemplate` (custom Template object)
    //, mode: "compact"     // special compact mode : hide messages & compact graph
};
var gitGraph = new GitGraph({
    //mode: "compact",
    template: "metro"
});

var selectedCommit;
var coordinateX = 999;
var coordinateY = 999;

gitGraph.template = myTemplate;

/***********************
 * BRANCHS AND COMMITS *
 ***********************/

// Create branch named "master"
var master = gitGraph.branch("master");
gitGraph.commit({
    message: "<b>Initial commit</b><br>",
    onClick: function(commit) { comTouch(commit); },
    changes: "Initial",
    dotColor: "#D43A3A"
});

master.commit({
    message: "<b>Commit message</b><br><i>Cell C28 = Ins.Row</i>",
    onClick: function (commit) { comTouch(commit); },
    changes: "Insert Row",
    dotColor: "#D43A3A"
});

var dev = gitGraph.branch("dev");
dev.commit({
    message: "<b>Commit message</b><br><i>Cell D31 = 400</i>",
    onClick: function (commit) { comTouch(commit); },
    changes: "Edit Cell|D31|450|400",
    dotColor: "#D43A3A"
});

dev.commit({
    message: "<b>Commit message</b><br><i>Cell D31 = 400</i>",
    onClick: function (commit) { comTouch(commit); },
    changes: "Edit Cell|D31|450|400",
    dotColor: "#D43A3A"
});
dev.merge(master, { dotColor: "#D43A3A" });

master.commit({
    message: "<b>Commit message</b><br><i>Cell F31 = Yellow tinted</i>",
    onClick: function (commit) { comTouch(commit); },
    changes: "Format Cell|F31||Yellow",
    dotColor: "#D43A3A"
});
var altuser = master.branch("altuser");
dev.commit({
    message: "<b>Commit message</b><br><i>Cell D31 = Yellow tinted</i>",
    onClick: function (commit) { comTouch(commit); },
    changes: "Format Cell|D31||Yellow"
});
altuser.commit({
    message: "<b>Commit message</b><br><i>Cell D31 = Yellow tinted</i>",
    onClick: function (commit) { comTouch(commit); },
    changes: "Format Cell|D31||Yellow"
});
altuser.commit({
    message: "<b>Commit message</b><br><i>Cell D31 = Yellow tinted</i>",
    onClick: function (commit) { comTouch(commit); },
    changes: "Format Cell|D31||Yellow"
});


dev.commit({
    message: "<b>Commit message</b><br><i>Cell D31 = Yellow tinted</i>",
    onClick: function (commit) { comTouch(commit); },
    changes: "Format Cell|D31||Yellow"
});
dev.commit({
    message: "<b>Commit message</b><br><i>Cell C28 = 15/09/2001</i>",
    onClick: function (commit) { comTouch(commit); },
    changes: "Edit Cell|C28||15/09/2001"
});
dev.merge(master, { dotColor: "#D43A3A" });
altuser.commit({
    message: "<b>Commit message</b><br><i>Cell D31 = Yellow tinted</i>",
    onClick: function (commit) { comTouch(commit); },
    changes: "Format Cell|D31||Yellow"
});
/*gitGraph.commit({
    message: "<b>Commit message</b><br><i>Cell D28 = 350</i>",
    onClick: function (commit) { comTouch(commit); },
    changes: "Edit Cell|D28||350"
});
gitGraph.commit({
    message: "<b>Commit message</b><br><i>Cell E28 = =(D28-D27)/D27</i>",
    onClick: function (commit) { comTouch(commit); },
    changes: "Edit Cell|E28||=(D28-D27)/D27"
});
gitGraph.commit({
    message: "<b>Commit message</b><br><i>Cell F28 = 400</i>",
    onClick: function (commit) { comTouch(commit); },
    changes: "Edit Cell|F28||400"
});
gitGraph.commit({
    message: "<b>Commit message</b><br><i>Cell G28 = =(F28-F27)/F27</i>",
    onClick: function (commit) { comTouch(commit); },
    changes: "Edit Cell|G28||=(F28-F27)/F27"
});*/

var com = gitGraph.commits[gitGraph.commits.length - 1];
coordinateX = com.x;
coordinateX = com.y;
comTouch(com);


/***********************
 *       EVENTS        *
 ***********************/

gitGraph.canvas.addEventListener("commit:mouseover", function (event) {
    console.log("You're over a commit.", "Here is a bunch of data ->", event.data);
});

// Attach a handler to the commit

function comTouch(commit) {
    console.log("You just clicked my commit.", commit);
    commit.dotColor = "white";
    commit.dotStrokeWidth = 10;

    if (selectedCommit != null && commit != selectedCommit) {
        selectedCommit.dotColor = selectedCommit.dotStrokeColor;
        selectedCommit.dotStrokeWidth = null;
        selectedCommit.parent.render();

        if (commit.changes != null) {
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
            console.log(final);
            window.external.ExecuteMacro(final);
        }
    }
    selectedCommit = commit;

    commit.parent.render();
}