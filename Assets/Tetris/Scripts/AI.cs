public class AI {
	public double heightWeight, linesWeight, holesWeight, bumpinessWeight;
	public AI(double heightWeight, double linesWeight, double holesWeight, double bumpinessWeight) {
		this.heightWeight = heightWeight;
		this.linesWeight = linesWeight;
		this.holesWeight = holesWeight;
		this.bumpinessWeight = bumpinessWeight;
	}
	public class Result {
		public AIPiece piece;
		public double score;
		public int rotation;
		public Result(AIPiece piece, double score, int rotation) {
			this.piece = piece;
			this.score = score;
			this.rotation = rotation;
		}
	}

	public AI.Result Best(SimpleGrid grid, Tetromino workingPiece){
		AIPiece best = null;
		var bestScore = 0.0;
		var aiPiece = new AIPiece[4];
		var bestRotation = 0;
		for (var rotation = 0; rotation < 4; rotation++) {
			workingPiece.transform.Rotate(0, 0, 90);
			aiPiece[rotation] = workingPiece.ToAIPiece();
		}
		workingPiece.transform.Rotate(0, 0, 90);	
		for(var rotation = 0; rotation < 4; rotation++){
			var _piece = aiPiece[rotation];

			while(grid.canMoveLeft(_piece)){
				_piece.column --;
			}

			while(grid.valid(_piece)){
				var _pieceSet = _piece.Clone();
				while(grid.canMoveDown(_pieceSet)){
					_pieceSet.row++;
				}

				var _grid = grid.Clone();
				_grid.addPiece(_pieceSet);

				var score = -this.heightWeight * _grid.aggregateHeight() + this.linesWeight * _grid.lines() - this.holesWeight * _grid.holes() - this.bumpinessWeight * _grid.bumpiness();

				if (score > bestScore || best == null){
					bestScore = score;
					best = _piece.Clone();
					bestRotation = rotation;
				}

				_piece.column++;
			}
		}

		return new Result(best, bestScore, bestRotation);
	}
}